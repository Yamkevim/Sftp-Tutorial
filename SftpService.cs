using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace SFTPService
{
    public interface ISftpService
    {
        IEnumerable<SftpFile> ListAllFiles(string remoteDirectory = "/homolagacao");
        void UploadFile(string localFilePath, string remoteFilePath);


    }

    public class SftpService : ISftpService
    {
        private readonly ILogger<SftpService> _logger;
        private readonly SftpConfig _config;

        public SftpService(ILogger<SftpService> logger, SftpConfig sftpConfig)
        {
            _logger = logger;
            _config = sftpConfig;
        }

        public IEnumerable<SftpFile> ListAllFiles(string remoteDirectory = "/homologacao")
        {
            using var client = new SftpClient(_config.Host, _config.Port, _config.UserName, _config.Password);
            try
            {
                client.Connect();
                return client.ListDirectory(remoteDirectory);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Falha ao gravar  [{remoteDirectory}]");
                return null;
            }
            finally
            {
                client.Disconnect();
            }
        }

        public void UploadFile(string filePath, string fileName)
        {
            using var client = new SftpClient(_config.Host, _config.Port, _config.UserName, _config.Password);
            {
                client.Connect();

                if (client.IsConnected)
                {
                    using (FileStream filestream = File.OpenRead(filePath))
                    {
                        client.UploadFile(filestream, fileName, null);

                    }
                }
                client.Disconnect();
            }
        }
        // public void UploadFile(string filePath, string fileName)
        // {
        //     using var client = new SftpClient(_config.Host,  _config.Port, _config.UserName, _config.Password);
        //     client.Connect();
        //     using (FileStream filestream = File.OpenRead(filePath))

        //     client.UploadFile(filestream, fileName, null);
        //     client.Disconnect();

        // }






    }

}
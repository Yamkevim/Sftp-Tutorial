using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;

using SFTPService;

namespace sftp
{
    class Program

    {
        private static async Task Main()
        {
            var config = new SftpConfig
            {
                Host = "10.100.0.37",
                Port = 22,
                UserName = "dev",
                Password = "homolog"

            };
            var sftpService = new SftpService(new NullLogger<SftpService>(), config);

            // list files
            // var files = sftpService.ListAllFiles("/");
            // foreach (var file in files)
            // {
            //     if (file.IsDirectory)
            //     {
            //         Console.WriteLine($"Directory: [{file.FullName}]");

            //     }
            //     else if (file.IsRegularFile)
            //     {
            //         Console.WriteLine($"File: [{file.FullName}]");
            //     }
            // }
            var path = "/homologacao"; 
            var nomeArquivo = "teste";
            
 
            var testFile =  await File.ReadAllBytesAsync("C://Users//yam.claro//Documents//Projetos//ExemploSftp//Tutorialsftp_ssh.sftp//bin//Debug//net6.0//teste1.txt");
            //sftpService.UploadFile(testFile, "/.");
            var file = await File.ReadAllBytesAsync("C://Users//yam.claro//Documents//Projetos//ExemploSftp//Tutorialsftp_ssh.sftp//bin//Debug//net6.0//teste1.txt");
            
            
            List<byte[]> list = new List<byte[]>();
            list.Add(file);
            var filePath = Path.Combine(list, "teste.txt");
            
            await sftpService.UploadFile(filePath, filePath  );
            

        }
    }
}

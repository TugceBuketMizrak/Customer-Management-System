using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace CustomerManagementSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //iss kullanıyosanız iss consoleu baslatır
            //iss kullanmıyosanız kernel suncuusnu kullanır ve web sunucusu düzeneği kurur
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                //startupta yaptığımız ayarları dikkate alması için yazılır
                .UseApplicationInsights()
                .Build();

            //yukarıdaki kod bize web sunucusu düzeneği kurara
            //genelde bu dosyaya dokunulmaz 
            host.Run();
        }
    }
}

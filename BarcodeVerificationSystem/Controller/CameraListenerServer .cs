using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BarcodeVerificationSystem.Controller;
using Newtonsoft.Json;
using Security;

namespace BarcodeVerificationSystem
{
    public class CameraListenerServer
    {
        private static HttpListener listener;

        public CameraListenerServer(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
                throw new Exception("Not support HttpListener.");

            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");
            
            listener = new HttpListener();
            foreach (string prefix in prefixes)
                listener.Prefixes.Add(prefix);

        }

        public async Task StartAsync()
        {
            listener.Start();
            do
            {
                try
                {
                    //Console.WriteLine($"{DateTime.Now.ToLongTimeString()} : waiting a client connect");
                    
                    HttpListenerContext context = await listener.GetContextAsync();
                    await ProcessRequest(context);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //Console.WriteLine("...");

            }
            while (listener.IsListening);
        }
        
        static async Task ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;
            Console.WriteLine($"{request.HttpMethod} {request.RawUrl} {request.Url.AbsolutePath}");
            
            var outputstream = response.OutputStream;
            
            var url = request.Url.AbsolutePath + request.Url.Query;
            switch (url)
            {
                case "/stop":
                    {
                        listener.Stop();
                        Console.WriteLine("stop http");
                    }
                    break;

                case "/api/request?act=get_printer_info":
                    {
                        response.Headers.Add("Content-Type", "application/json");
                       var cameraBoxInfo = new ResponseGetInfoModel
                        {
                            success = "true",
                            message = "Get printer info successful",
                            data = new GetInfoDataResponseModel
                            {
                                model = Properties.Settings.Default.SoftwareModel,
                                uuid = FingerPrint.Value(),
                                software = Properties.Settings.Default.SoftwareVersion,
                                // 0 Undefine
                                // 1 - Start
                                // 2 - stop
                                // 3 - Processing
                                // 4 - Resumer
                                // 5 - reset default
                                // 6 - Waiting data
                                printStatus = (int)Shared.OperStatus,
                                printerName = "My RLink"
                            }
                        };
                        string jsonstring = JsonConvert.SerializeObject(cameraBoxInfo);
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(jsonstring);
                        response.ContentLength64 = buffer.Length;
                        await outputstream.WriteAsync(buffer, 0, buffer.Length);

                    }
                    break;

                default:
                    {
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes("NOT FOUND!");
                        response.ContentLength64 = buffer.Length;
                        await outputstream.WriteAsync(buffer, 0, buffer.Length);
                    }
                    break;
            }
            
            outputstream.Close();
        }
    }
}

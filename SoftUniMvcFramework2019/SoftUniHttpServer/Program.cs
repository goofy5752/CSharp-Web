﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SoftUniHttpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            const string NewLine = "\r\n";
            var tcpListener = new TcpListener(
                IPAddress.Loopback, 80);

            tcpListener.Start();
            // WebUtility.UrlDecode
            while (true)
            {
                // TCP / UDP
                var client = tcpListener.AcceptTcpClient();
                using (var stream = client.GetStream())
                {
                    var requestBytes = new byte[100000];
                    // Thread.Sleep(10000);
                    var readBytes = stream.Read(requestBytes, 0, requestBytes.Length);
                    var stringRequest = Encoding.UTF8.GetString(requestBytes, 0, readBytes);
                    Console.WriteLine(new string('=', 70));
                    Console.WriteLine(stringRequest);
                    var responseBody = "<form method='post'><input type='text' name='tweet' placeholder='Enter tweet...' /><input name='name' /><input type='submit' /></form>";
                    var response = "HTTP/1.0 200 OK" + NewLine +
                                      "Content-Type: text/html" + NewLine +
                                      // "Location: https://google.com" + NewLine +
                                      // "Content-Disposition: attachment; filename=index.html" + NewLine +
                                      "Server: MyCustomServer/1.0" + NewLine +
                                      $"Content-Length: {responseBody.Length}" + NewLine + NewLine +
                                      responseBody;
                    var responseBytes = Encoding.UTF8.GetBytes(response);
                    stream.Write(responseBytes, 0, responseBytes.Length);
                }
            }
        }
    }
}
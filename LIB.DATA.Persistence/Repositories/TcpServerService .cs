using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIB.API.Persistence.Repositories
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class TcpServerService : BackgroundService
    {
        private readonly ILogger<TcpServerService> _logger;
        private TcpListener _listener;
        private readonly int _port = 5050;

        public TcpServerService(ILogger<TcpServerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener = new TcpListener(IPAddress.Any, _port);
            _listener.Start();
            _logger.LogInformation($"TCP Server started on port {_port}");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    _logger.LogInformation($"Client connected: {client.Client.RemoteEndPoint}");
                    _ = Task.Run(() => HandleClient(client, stoppingToken));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"TCP Server error: {ex.Message}");
                }
            }
        }

        private async Task HandleClient(TcpClient client, CancellationToken token)
        {
            using var stream = client.GetStream();
            var buffer = new byte[1024];

            try
            {
                while (!token.IsCancellationRequested)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                    if (bytesRead == 0) break;

                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead).Trim();
                    _logger.LogInformation($"Received: {receivedMessage}");

                    string response = ProcessCommand(receivedMessage);
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length, token);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Client error: {ex.Message}");
            }
            finally
            {
                client.Close();
                _logger.LogInformation("Client disconnected.");
            }
        }

        private string ProcessCommand(string command)
        {
            return command.ToUpper() switch
            {
                "GET_TEMP" => "Temperature = 25.3°C",
                "GET_STATUS" => "Status = Active",
                _ => "Unknown command"
            };
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _listener?.Stop();
            _logger.LogInformation("TCP Server stopped.");
            await base.StopAsync(stoppingToken);
        }
    }

}

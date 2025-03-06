using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

[ApiController]
[Route("api/tcp")]
public class TcpController : ControllerBase
{
    private readonly string _serverIP = "127.0.0.1"; // Localhost
    private readonly int _serverPort = 5050; // Must match your TCP server port

    [HttpGet("send")]
    public async Task<IActionResult> SendCommand([FromQuery] string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return BadRequest("Command cannot be empty");

        try
        {
            using TcpClient client = new TcpClient(_serverIP, _serverPort);
            using NetworkStream stream = client.GetStream();

            byte[] data = Encoding.UTF8.GetBytes(command);
            await stream.WriteAsync(data, 0, data.Length);

            byte[] buffer = new byte[1024];
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            return Ok(new { Command = command, Response = response });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error: {ex.Message}");
        }
    }
}

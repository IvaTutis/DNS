

using System.Net;
using System.Net.Sockets;
using DomainNameServer;
using Simulator;

const ushort TYPE_A = 1; // Define TYPE_A as a constant with value 1, representing IPv4 address records
var domainName = "www.example.com"; // The domain name we want to look up
var query = DnsUtility.BuildQuery(domainName, TYPE_A); // Build a DNS query for the specified domain name, requesting an A (IPv4) record

// Create a new UDP client for sending and receiving DNS packets
using (var udpClient = new UdpClient())
{
    var endpoint = new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53);
    udpClient.Send(query, query.Length, endpoint);

    var response = udpClient.Receive(ref endpoint);
    Console.WriteLine($"Received {response.Length} bytes from DNS server");

    string ipAddress = DnsResponseParser.ParseDnsResponse(response);
    Console.WriteLine($"IP address for {domainName}: {ipAddress}");
}


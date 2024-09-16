using System.Net;

namespace Simulator;

public static class DnsResponseParser
{
    public static string ParseDnsResponse(byte[] response)
    {
        // Skip the header (12 bytes) and question section (variable length)
        int index = 12;

        // Skip the question section
        while (response[index] != 0)
        {
            index += response[index] + 1;
        }
        index += 5; // Skip the null byte, type, and class

        // Parse the answer section
        if (index < response.Length)
        {
            // Skip the name pointer
            index += 2;

            // Read type
            ushort type = BitConverter.ToUInt16(new byte[] { response[index + 1], response[index] });
            index += 2;

            // Skip class
            index += 2;

            // Skip TTL
            index += 4;

            // Read data length
            ushort dataLength = BitConverter.ToUInt16(new byte[] { response[index + 1], response[index] });
            index += 2;

            if (type == 1 && dataLength == 4) // TYPE A (IPv4 address)
            {
                byte[] ipBytes = new byte[4];
                Array.Copy(response, index, ipBytes, 0, 4);
                return new IPAddress(ipBytes).ToString();
            }
        }

        return "Unable to parse DNS response";
    }
}
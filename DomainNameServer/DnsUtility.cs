using System.Text;

namespace DomainNameServer;


using System.Text;

public static class DnsUtility
{
    /// <summary>
    /// Encodes a domain name into the DNS format.
    /// </summary>
    /// <param name="domainName">The domain name to encode (e.g., "example.com")</param>
    /// <returns>A byte array representing the encoded domain name</returns>
    public static byte[] EncodeDomainName(string domainName)
    {
        var result = new List<byte>();
        // Split the domain name into parts (e.g., ["example", "com"])
        foreach (var part in domainName.Split('.'))
        {
            // Add the length of each part as a byte
            result.Add((byte)part.Length);
            // Add the ASCII bytes of the part itself
            result.AddRange(Encoding.ASCII.GetBytes(part));
        }
        // Add a zero byte to terminate the domain name
        result.Add(0);
        return result.ToArray();
    }

    /// <summary>
    /// Builds a complete DNS query for a given domain name and record type.
    /// </summary>
    /// <param name="domainName">The domain name to query</param>
    /// <param name="recordType">The type of DNS record to request (e.g., A, AAAA, MX)</param>
    /// <returns>A byte array representing the complete DNS query</returns>
    public static byte[] BuildQuery(string domainName, ushort recordType)
    {
        var random = new Random();
        // Generate a random 16-bit ID for the query
        var id = (ushort)random.Next(ushort.MaxValue);
        // Set the recursion desired flag
        const ushort recursionDesired = 1 << 8;

        // Create the DNS header
        var header = new DnsHeader()
        {
            Id = id,
            Flags = recursionDesired,
            NumQuestions = 1  // We're asking one question in this query
        };

        // Create the DNS question
        var question = new DnsQuestion()
        {
            Name = EncodeDomainName(domainName),
            Type = recordType,
            Class = 1 // IN (Internet)
        };

        // Combine the header and question into a single byte array
        return header.ToBytes().Concat(question.ToBytes()).ToArray();
    }
}
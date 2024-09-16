namespace DomainNameServer;

public class DnsQuestion
{
    public byte[] Name { get; set; }
    public ushort Type { get; set; }
    public ushort Class { get; set; }

    public byte[] ToBytes()
    {
        var typeBytes = BitConverter.GetBytes(Type);
        var classBytes = BitConverter.GetBytes(Class);
        return Name.Concat(typeBytes).Concat(classBytes).ToArray();
    }
}
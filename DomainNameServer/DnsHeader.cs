namespace DomainNameServer;

public class DnsHeader
{
    public ushort Id { get; set; }
    public ushort Flags { get; set; }
    public ushort NumQuestions { get; set; }
    public ushort NumAnswers { get; set; }
    public ushort NumAuthorities { get; set; }
    public ushort NumAdditionals { get; set; }

    public byte[] ToBytes()
    {
        var bytes = new byte[12];
        BitConverter.GetBytes(Id).CopyTo(bytes, 0);
        BitConverter.GetBytes(Flags).CopyTo(bytes, 2);
        BitConverter.GetBytes(NumQuestions).CopyTo(bytes, 4);
        BitConverter.GetBytes(NumAnswers).CopyTo(bytes, 6);
        BitConverter.GetBytes(NumAuthorities).CopyTo(bytes, 8);
        BitConverter.GetBytes(NumAdditionals).CopyTo(bytes, 10);
        return bytes;
    }
}
namespace Recaster.Common
{
    public class QualifierSettings
    {
        public string SourceIp { get; set; } = "::1";
        public int SourcePort { get; set; } = 0;
        public bool Discard { get; set; } = true;
    }
}

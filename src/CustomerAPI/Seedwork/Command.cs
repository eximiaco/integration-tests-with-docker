using System;

namespace CustomerAPI.Seedwork
{
    public abstract class Command
    {
        public Command()
        {
            Issued = DateTimeOffset.UtcNow;
        }
        
        public DateTimeOffset Issued { get; set; }
    }
}

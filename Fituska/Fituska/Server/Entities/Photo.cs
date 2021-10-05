using System;

namespace Fituska.Server.Entities
{
    public class Photo
    {
        public Guid Id {  get; set; }

        public byte[] Content {  get; set; }
    }
}

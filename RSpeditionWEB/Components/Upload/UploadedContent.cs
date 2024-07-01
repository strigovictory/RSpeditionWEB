using Newtonsoft.Json.Linq;
using RSpeditionWEB.Models.RequestModels;
using System.Reflection.Metadata;

namespace RSpeditionWEB.Components.Upload
{
    public class UploadedContent : RequestBase, ICloneable, IUploadedContent
    {
        public UploadedContent()
            : base () 
        { }

        public UploadedContent(string userName)
            : base(userName)
        { }

        [JsonIgnore]
        public string FilePath { get; set; }

        [JsonInclude]
        public byte[] Content { get; protected set; }

        [JsonInclude]
        public string FileName { get; protected set; }

        public virtual object Clone()
        => new UploadedContent
        {
            FilePath = this.FilePath,
            Content = this.Content,
            FileName = this.FileName,
            UserName = this.UserName,
        };

        public void InitContent()
        {
            if(string.IsNullOrEmpty(this.FilePath))
            {
                this.Content = new byte[0]; 
                this.FileName = string.Empty; 
            }
            else
            {
                using FileStream fs = new(this.FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                using BinaryReader binaryReader = new(fs);
                this.Content = binaryReader.ReadBytes((Int32)(new FileInfo(this.FilePath).Length));
                this.FileName = string.IsNullOrEmpty(this.FilePath) ? string.Empty : new FileInfo(this.FilePath).Name ?? String.Empty;
            }
        }
    }
    public interface IUploadedContent
    {
        string FilePath { get; set; }
        byte[] Content { get; }
        string FileName { get; }
        void InitContent();
    }
}

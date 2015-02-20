using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace KaReMa.Interfaces
{
    [DataContract]
    public class MetaInfoDTO
    {
        public MetaInfoDTO()
        {
            //this.TimeCreated = DateTime.Now;
            //this.Author = System.Environment.UserName;
            //this.Tags = new List<Guid>();
            this.ImageData = new ImageData();
        }

        //public MetaInfoDTO(DateTime lastTimeChanged, DateTime timeCreated, List<Guid> tags, String author, String hint, ImageData imageData)
        //{
        //    this.LastTimeChanged = lastTimeChanged;
        //    this.TimeCreated = timeCreated;
        //    this.Tags = tags;
        //    this.Author = author;
        //    this.Hint = hint;
        //    this.ImageData = imageData;
        //}


        public ImageData ImageData
        {
            get;
            set;
        }


        [DataMember]
        public DateTime LastTimeChanged
        {
            get;
            set;
        }

        [DataMember]
        public DateTime TimeCreated
        {
            get;
            set;
        }

        [DataMember]
        public List<Guid> Tags
        {
            get;
            set;
        }

        [DataMember]
        public String Author
        {
            get;
            set;
        }

        [DataMember]
        public String Hint
        {
            get;
            set;
        }
    }
}

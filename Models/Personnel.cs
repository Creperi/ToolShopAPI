using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToolShopAPI.Models;
    public class Personnel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FullName { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime AdditionDate { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime AssignToolDate { get; set; }
        [BsonElement("SelectedTools")]
        public List<Tool> SelectedTools { get; set; }
        public Personnel() { }

        public void AssignTools(List<Tool> tools)
        {
            SelectedTools = tools;
        }

    }

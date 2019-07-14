using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace GamerPalsBackend.DataObjects.Models
{
    public class ParameterPossibility
    {
        public string possibilityName { get; set; }
        public object possibilityValue { get; set; }
    }

    public class SearchParameter : IModelBase
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string ParameterName { get; set; }
        public string parameterGrayText { get; set; }
        public string parameterHint { get; set; }
        public string parameterType { get; set; }
        public object parameterValueFrom { get; set; }
        public object parameterValueTo { get; set; }
        public object parameterValue { get; set; }
        public List<ParameterPossibility> parameterPossibilities { get; set; }
    }
}

using Feng.Core;
using Newtonsoft.Json;

namespace Feng.Basic.Serialize
{
    public class DefaultJsonHelper : IJsonHelper
    {
        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }

        public string SerializeObject(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}

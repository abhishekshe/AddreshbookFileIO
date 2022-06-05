
namespace AddressBook
{
    internal class JsonTextWriter
    {
        private StreamWriter writerJson;

        public JsonTextWriter(StreamWriter writerJson)
        {
            this.writerJson = writerJson;
        }
    }
}
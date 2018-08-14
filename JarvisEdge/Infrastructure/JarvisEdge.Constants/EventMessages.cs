namespace JarvisEdge.Constants
{
    public static class EventMessages
    {
        public static string Change(string property, string from, string to)
        {
            return string.Format("{0} changed from {1} to {2}", property, from, to);
        }

        public static string Created(string objectName)
        {
            return string.Format("{0} has been created", objectName);
        }
    }
}

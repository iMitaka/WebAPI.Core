namespace JarvisEdge.Constants.User
{
    public static class UserTypeTrading
    {
        private const string scalping = "scalping";
        private const string dayTrading = "dayTrading";
        private const string swingTrading = "swingTrading";
        private const string carryTrading = "carryTrading";
        private const string longTerm = "longTerm";

        public static string Scalping()
        {
            return scalping;
        }

        public static string DayTrading()
        {
            return dayTrading;
        }

        public static string SwingTrading()
        {
            return swingTrading;
        }

        public static string CarryTrading()
        {
            return carryTrading;
        }

        public static string LongTerm()
        {
            return longTerm;
        }
    }
}

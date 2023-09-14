namespace Journey
{
    public class Step
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string? Inputs { get; set; }
        public Responses Responses { get; set; }
        public Functions? Functions { get; set; }
        //public string? Functions { get; set; }
        public string? Reward { get; set; }
    }

    public class Responses
    {
        public string Name { get; set; }
        public string Next { get; set; }

    }

    // TODO: consider making this a string and not class if not needed
    public class Functions
    {
        public string Name { get; set; }
    }

}

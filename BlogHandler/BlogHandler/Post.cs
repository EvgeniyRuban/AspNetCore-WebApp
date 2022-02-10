namespace BlogHandler
{
    internal class Post
    {
        public long UserId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public override string ToString()
        {
            return $"{UserId}\n" +
                $"{Id}\n" +
                $"{Title}\n" +
                $"{Body}";
        }
    }
}

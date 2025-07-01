namespace NoteApi.DTOs
{
    public class NoteCreateDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
        public int UserId { get; set; }
    }

    public class NoteUpdateDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
}

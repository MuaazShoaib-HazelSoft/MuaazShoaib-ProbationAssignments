namespace UserManagementSystem.DTOS.CoursesDTO
{
    /// <summary>
    /// Dto for Getting 
    /// Courses Details.
    /// </summary>
    public class GetCoursesDto
    {
        public int Id { get; set; }
        public string CourseName { get; set; }
        public List<string> Users { get; set; }
    }
}

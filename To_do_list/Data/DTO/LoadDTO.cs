using To_do_list.Data.Entities.Enums;

namespace To_do_list.Data.DTO
{
    public class LoadDTO
    {
        public string Description { get; set; }
        public Statuses Status { get; set; }
    }
}
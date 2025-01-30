using To_do_list.Data.Entities.Enums;

namespace To_do_list.Data.Entities;

public class Tasks
{ 
    public Guid Id { get; set; }
    public string Description { get; set; }
    public Statuses Status { get; set; }
}
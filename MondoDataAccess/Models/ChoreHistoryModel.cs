namespace MondoDataAccess.Models;
public class ChoreHistoryModel
{
    public ChoreHistoryModel()
    {
    }

    public ChoreHistoryModel(ChoreModel chore)
    {
        Id = chore.Id;
        DateCompleted = chore.LastCompleted ?? DateTime.Now;
        WhoCompleted = chore.AssignedTo;
        ChoreText = chore.ChoreText;
    }

    public string Id { get; set; }

    public string ChoreId { get; set; }

    public string ChoreText { get; set; }

    public DateTime DateCompleted { get; set; }

    public UserModel? WhoCompleted { get; set; }
}

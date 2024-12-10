namespace WebShop;

public interface IModel<TId>
{
    public TId Id { get; set; } 
}
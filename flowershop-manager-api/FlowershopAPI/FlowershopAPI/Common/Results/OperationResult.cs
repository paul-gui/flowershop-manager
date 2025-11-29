namespace FlowershopAPI.Common.Results;

public class OperationResult<T>
{
    public bool Succeeded { get; set; }
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
    
    public static OperationResult<T> Success(T data) => new() {Succeeded = true, Data = data};
    public static OperationResult<T> Failed(List<string> errors) => new() {Succeeded = false, Errors = errors };
}
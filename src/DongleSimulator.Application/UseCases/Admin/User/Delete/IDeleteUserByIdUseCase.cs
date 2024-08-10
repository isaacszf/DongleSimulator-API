namespace DongleSimulator.Application.UseCases.Admin.User.Delete;

public interface IDeleteUserByIdUseCase {
    public Task Execute(string id);
}
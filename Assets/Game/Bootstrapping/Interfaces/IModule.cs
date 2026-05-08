
public interface IModule
{
    void SetTrigers(UpdatePhaseDesc phase); // запускают действия
    void SetValidation(UpdatePhaseDesc phase); // проверяют возможность действия
    void SetLogic(UpdatePhaseDesc phase); // выполняют действия
    void SetView(UpdatePhaseDesc phase); // пост эффекты
}

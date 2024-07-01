namespace RSpeditionWEB.Components.Upload
{
    public abstract class UploadFilesBase<T>: GroupEditFormBase<T> where T: class, ICloneable, IUploadedContent, new()
    {
        protected UploadFilesBase() : base() { }

        public abstract string ClassForUploadForm { get; }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();
            this.InitButtons();
        }

        protected abstract Task InitializeCollections();

        private void DelFileFromTempDir()
        {
            if (!string.IsNullOrEmpty(this.Model.FilePath) && File.Exists(this.Model.FilePath))
                File.Delete(this.Model.FilePath);

            this.Model.FilePath = string.Empty;
        }

        protected async Task ReactClickOnUploadFiles()
        {
            this.Message = String.Empty;

            if (!string.IsNullOrEmpty(this.Model.FilePath) && File.Exists(this.Model.FilePath))
            {
                if ((this.Model?.Content?.Length ?? 0) > 0)
                {
                    await this.ProgressBarOpenAsync();

                    // Задача по парсингу отчета
                    var taskSendFile = Task.Run(async () => await this.StartOperation());

                    // Запустить таймер
                    this.TimeStartProcess = DateTime.Now;

                    // Продолжить работу после завершения выполнения всех методов, передаваемых в качестве параметров в метод  WhenAll()
                    await Task.WhenAll(new[] { taskSendFile });

                    this.Message = this.OperationsResult?.Result ?? string.Empty;

                    if (this.OperationsResult == null)
                    {
                        this.Title = "Предупреждение";
                        this.Message += "Возникла ошибка! Операция не может быть выполнена !";
                    }
                    else
                    {
                        this.Message = this.OperationsResult?.Result ?? String.Empty;
                        if (this.OperationsResult != null)
                        {
                            this.ShouldUpdateParent = true;
                            await this.ProccessingResponse();
                            this.InitButtons();
                        }
                        else
                        {
                            this.Title = "Предупреждение";
                            this.Message += $"Возникла ошибка!";
                        }
                    }

                    // Остановить таймер
                    this.TimeFinishProcess = DateTime.Now;
                    var process = (this.TimeFinishProcess - this.TimeStartProcess);
                    this.TimeProcessMin = process.Minutes;
                    this.TimeProcessSec = process.Seconds;
                    await this.ProgressBarCloseAsync();
                    this.Message += $"Процесс длился {this.TimeProcessMin} мин. {this.TimeProcessSec} сек.!";
                    this.RenderMessage();
                }
                else
                {
                    this.Message = "Файл не удалось загрузить!";
                    this.Title = "Предупреждение";
                    this.RenderMessage();
                }

                // Удалить файл из врем.папки после обработки файла
                this.DelFileFromTempDir();
            }
            else
            {
                this.Message = "Файл не был прикреплен!";
                this.Title = "Предупреждение";
                this.RenderMessage();
            }
        }
    }
}

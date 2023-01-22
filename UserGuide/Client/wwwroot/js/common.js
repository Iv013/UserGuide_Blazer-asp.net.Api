window.ShowToastr = (Result, message) =>
{
    if (Result === 200) {
      toastr.success(message, "Выполнено", { timeout: 5000 });

    } 
    if (Result === 409)
    {
        toastr.error(message, "Ошибка", { timeout: 5000 });
    }
      
    if (Result === 102) {
        toastr.warning(message, "Предупреждение", { timeout: 5000 });
    }
}
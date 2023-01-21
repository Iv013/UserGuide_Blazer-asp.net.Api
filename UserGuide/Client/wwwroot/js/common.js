  window.ShowToastr = (Res, message) => {
    if (Res === true) {
        toastr.success(message, "Выполнено", { timeout: 5000 });   
      
    }
    if (Res === false) {
        toastr.error (message, "Ошибка", { timeout: 5000 });
    }
}
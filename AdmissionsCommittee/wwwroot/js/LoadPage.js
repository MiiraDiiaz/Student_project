$('.transition').click(function (e) {//вешаем click на все элементы с классом  to_top
    e.preventDefault(); //отменяем действие по умолчанию(переход по ссылке)
    $("#Content").load($(this).attr('href'));// добавляем в контейнер с id=divContent,  html  загруженный с  помощью  load
   
});
пользователь: логин- mumu@gmail.com пароль- Usermumu1!

чтобы содержимое гугл-карт отображалось корректно (для нормальной работы приложения не обязательно),
нужно гугл-ключ вставить в представление map.cshtml:

@*INSERT YOUR GOOGLE KEY INSTEAD OF MINE ........ *@
@*TO GET GOOGLE KEY USE ACCOUNT GMAIL AND GO THERE - https://developers.google.com/maps/documentation/javascript/*@
<script async defer src="https://maps.googleapis.com/maps/api/js?key=........&callback=initMap"></script>

графики статистики работают, но через раз: кликните повторно, если не отрисовалось изображение кривых

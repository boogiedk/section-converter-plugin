# section-converter-plugin-for-AutoCAD
This is my first big project, the meaning of which is to work with the api AutoCAD app. Details can be found in the ReadMe.


Приветсвую, {userName}!

  Данная статья является описанием моего первого крупного проекта. В ней будут описаны основные функции, интерфейс и результат работы плагина. 

1. Загрузка плагина при старте работы Автокада.

  Чтобы плагин загружался при запуске автокада, я добавил в папку с автокадом "acadoc.lsp" файл с параметрами загрузки dll сборки.
Код lisp файла выглядел примерно так:

```
   	(
    defun-q HwdStartup()
    (c:AcadLayoutsCountLoad)
   	)  
   	(
    	defun c:AcadLayoutsCountLoad()
   (setq echo (getvar "cmdecho"))
   (setvar "cmdecho" 0)
  (setq fd (getvar "filedia"))
  (setvar "filedia" 0)
   (command "_netload" "path to dll file")
    (setvar "filedia" fd)
   (setvar "cmdecho" echo)
	)

  (setq S::STARTUP (append S::STARTUP HwdStartup))
  ```
  
  2. Загрузка кнопок в ленту автокада и их краткий обзор.
    
    После успешного подключения dll файла, пользователь увидит новую вкладку в ленте с инструментами автокада - Конвертер сечений.
 
![Ribbon](https://github.com/boogiedk/section-converter-plugin-for-AutoCAD/raw/master/resourceReadme/main_ribbon.png)

В ленте можно увидеть 4 вида точек - Ось, Отметка, Черная и Красная (все они представляют из себя блоки с сущностями(entity) точка(DbPoint) + текст(MText)) -, настройки точек и настройка размера окна(об этом позже) и кнопку, которая позволяет сформировать документацию(xml,dwg,tsv) из данных о точках на чертеже. 

  3. Создание точек и как это выглядит в действии
  
    Собственно, каждая точка имеет индивидуальную семантику. Например, Осевая точка требует ввод в формате "ПК_123+12.345" и может быть только единственной на чертеже, когда Отметка имеет формат - "53м", например. Выглядят точки на чертеже примерно так:

![Ribbon](https://github.com/boogiedk/section-converter-plugin-for-AutoCAD/raw/master/resourceReadme/points.png)
![Ribbon](https://github.com/boogiedk/section-converter-plugin-for-AutoCAD/raw/master/resourceReadme/axisInfo.png)

  4. Экспорт данных и формирование документации.

  Когда на чертеже установлен набор точек(1 осевая, 1 отметка, >2 черных и любое множество красных), можно приступить к запуску экспорта данных. 
![Ribbon](https://github.com/boogiedk/section-converter-plugin-for-AutoCAD/raw/master/resourceReadme/export.png)
	В результате мы получаем такой набор выходных данных:
![Ribbon](https://github.com/boogiedk/section-converter-plugin-for-AutoCAD/raw/master/resourceReadme/work place.png)
 	data.xml - хранит в себе данные о точках: позиция, текст, номер.
![Ribbon](https://github.com/boogiedk/section-converter-plugin-for-AutoCAD/raw/master/resourceReadme/data.png)
	settings.xml - хранит в себе данные о чертеже и выходных файлов: имена, пути, форматы.
	<имя_чертежа>_list_<имя чертежа_время_создания>.tsv - хранит в себе данные о чертеже, и формирует входные данные для подпрограммы.
	<имя_чертежа>_blueprint_<имя чертежа_время_создания>.dwg - чертеж с графическим выводом результатов.
![Ribbon](https://github.com/boogiedk/section-converter-plugin-for-AutoCAD/raw/master/resourceReadme/result.png)




  

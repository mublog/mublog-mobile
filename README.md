# Mubloq Mobile
Dies ist ein Beispiel wie eine App aussehen könnte, welche an die Mubloq-API angebunden ist.
(In diesem Fall mit Xamarin-Forms erstellt).

![app icon](https://i.imgur.com/MNXu4mO.png)


## Die Menübar

über die Menübar kannn der Nutzer auf die verschiedenen Seiten, der App steuern.
Hierbei gibt es 2 verschiedene, einmal die FeedPage und die ProfilePage.

![menuBar](https://i.imgur.com/bRWOE19.png)

## Die FeedPage

Über diese können die neusten Posts, der Nutzer, sowie deren Likes eingesehen werden.
Über das klicken auf eines der Profilbilder gelangt der Nutzer auf die diesem User zugehörige ProfilePage.
Alternativ kann er auch zu seiner eigenen ProfilePage über die Menübar steuern.

![feedPage](https://i.imgur.com/1m5gGRM.png)


## Die ProfilePage

Diese zeigt alle Posts eines spezifischen Nutzers, sowie einen zugehörigen Header für diesen an.
Die Posts werden im gleichen Layout wie auf der FeedPage präsentiert.

![profilePage](https://i.imgur.com/CQK2YA9.png)

## Über die App
Die App wurde mit Xamarin-Forms entwickelt und ist somit prinzipell sowie für Android als auch IOS einsetzbar (allerdings bisher nur auf Android getestet).
![Projects](https://i.imgur.com/HKUtiV5.png)

### MVVM
In Xamarin lässt sich die MVVM-Architektur sehr gut nutzen. Hierbei wird die Struktur in Views, ViewModels und Models eingeteilt.

![mvvm structure](https://i.imgur.com/IpbEWLz.png)

Die Views fragen vom zugehörigen ViewModel Daten ab und geben Input an dieses weiter.
Das ViewModel behandelt dadrauf hin die Interaktion mit den Daten, den Models.
Gleichzeitig kann das ViewModel über DataBinding die Views über Datenänderung informieren.
So kann sichergestellt werden, dass Views immer die Backend-Daten repräsentieren und es eine Kopplung von UI und Backend gibt. 






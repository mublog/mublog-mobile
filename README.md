# Archival Notice
µblog was a school project to create a working prototype of a microblogging website.
No further development is currently planned
<br/>

# Mubloq Mobile
Dies ist ein Beispiel wie eine App aussehen könnte, welche an die Mublog-API angebunden ist.
(In diesem Fall mit Xamarin-Forms erstellt).

![app icon](https://i.imgur.com/MNXu4mO.png)

## Login

Der Nutzer kann sich über die LoginPage einloggen. Dies ermöglicht ihm seine eigene ProfilePage zu sehen.
Die Benutzung der App allerdings auch ohne Login eingeschränkt möglich.

![login](https://i.imgur.com/QXeEfvL.png)

## Die Menübar

über die Menübar kann auf die verschiedenen Seiten der App navigiert werden.
Je nachdem, ob der Nutzer eingeloggt ist hat, hat er die Möglichkeit auf Feed, Profile, und Login/Logout zuzugreifen.

![menuBar](https://i.imgur.com/NPWLNth.png)

## Die FeedPage

Über diese können die neusten Posts, der Nutzer, sowie deren Likes und Kommentare eingesehen werden.
Mit klicken auf eines der Profilbilder gelangt der Nutzer auf die diesem User zugehörige ProfilePage.
Alternativ, falls eingeloggt, kann er auch zu seiner eigenen ProfilePage über die Menübar zugreifen.
Über das Kommentar-Symbol ist es möglich auf die Kommentar-Sektion eines Postes zu steuern.


![feedPage](https://i.imgur.com/L1rlAJW.png)


## Die ProfilePage

Diese zeigt alle Posts eines spezifischen Nutzers, sowie einen zugehörigen Header für diesen an.
Die Posts werden im gleichen Layout wie auf der FeedPage präsentiert.

![profilePage](https://i.imgur.com/6Fukc2W.png)


## Die CommentPage

Auf dieser Seite sind alle Kommentare eines Postes sichtbar.

![profilePage](https://i.imgur.com/HQKx9p8.png)

## Über die App
Die App wurde mit Xamarin-Forms entwickelt und ist somit prinzipell sowie für Android als auch IOS einsetzbar (allerdings bisher nur auf Android getestet).

![Projects](https://i.imgur.com/HKUtiV5.png)

### MVVM
In Xamarin lässt sich die MVVM-Architektur sehr gut nutzen. Hierbei wird die Struktur in Views, ViewModels und Models eingeteilt.

![mvvm structure](https://i.imgur.com/J7FvTBK.png)

Die Views fragen vom zugehörigen ViewModel Daten ab und geben Input an dieses weiter.
Das ViewModel behandelt dadrauf hin die Interaktion mit den Daten, den Models.
Gleichzeitig kann das ViewModel über DataBinding die Views über Datenänderung informieren.

In der View wird beim Like auf das LikeTapCommand gebinded. 

![mvvm view](https://i.imgur.com/4w4UZKW.png)

Dieses kann nun im zugehörigen ViewModel abgearbeitet werden.

![mvvm viewmModel](https://i.imgur.com/XwaEYqG.png)

Auf diese Art kann sichergestellt werden, dass Views immer die Backend-Daten repräsentieren und es eine Kopplung von UI und Backend gibt.
Hiermit bleibt UI austauschbar.







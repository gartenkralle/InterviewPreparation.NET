//Component:
Template + Class + Decorator

//Decorators:
https://v2.angular.io/docs/ts/latest/api/core/index/Component-decorator.html

//Example:
import { Component } from "@angular/core"

//app.component.ts
@Component({ // decorator
  selector: 'my-app',
  templateUrl: 'app/app.component.html' // template
})
export class AppComponent { // class
  name: string = "Angular";
}

//app.component.html
<h1>
  Hello {{name}}
</h1>

//index.html
<!DOCTYPE html>
<html>
<head>...</head>
<body>
  <my-app>Loading...(this will be replaced)</my-app>
</body>
</html>

<html><head><meta http-equiv=Content-Type content="text/html; charset=UTF-8"></head>
<body>

<b>WC_IPADDRESS (SysIPAddress32) for .Net</b></br></br>


Hace más de veinte años, Microsoft desarrolló una Librería para facilitar y estandarizar la tarea de los desarrolladores a la hora de implementar en sus aplicaciones. En ella estaban encapsulados una serie de controles comunes con el objeto de que el desarrollador se dedicase a los entresijos del corazón de su aplicación, dejando la interacción con el usuario a una mera, facil y simple implementación de dichos controles.</br>
Con la llegada (hace 14 años) de la tecnología .NET y su CLR, se facilitó aún más esta implementación. Por ejemplo, para interaccionar con el usuario y presentarle los datos de una base de datos utilizamos un control ListView, sin necesidad de crear dicho interfaz.</br>
Es curioso cuando menos que el único control no incluido en .NET, sea WC_IPADDRESS (SysIPAddress32), y más curioso aún que en todo este tiempo no se halla desarrollado una implementación usable, simple y fácil del mismo, máxime teniendo en cuenta del tipo de utilidad que tiene: interaccionar con el usario sobre uno de los aspectos más básicos de las redes TCP/IP: su dirección IP.</br></br>

Esta imlementación pretende ser un acercamiento a esa implemetación fácil y simple.</br></br>

Desde el punto de vista técnico, se ha elegido tomar como base un TextBox y aprovechando la herencia de clases, implementar las funcionaidades incluidas en la librería original Common control.</br></br>

Los Clase Form en .NET no tiene las mismas características que una ventana de diálogo. Esto implica que Windows no interpreta correctamente las pulsaciones de teclas para la navegación a través de los distintos controles.</br>
Es por ello que se invalidan determinadas propedades, además de realizar subclass a los elementos necesarios.</br></br>


If you need a custom development, or any other issues, please contact via email.</br></br>

apifilmaffinityimdb[[at]]g m ail.com

</body></html>

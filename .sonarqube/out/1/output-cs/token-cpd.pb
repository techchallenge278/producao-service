¿
qD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Services\IOrderServiceClient.cs
	namespace 	
Producao
 
. 
Application 
. 
Services '
;' (
public 
	interface 
IOrderServiceClient $
{ 
Task 
< 	
bool	 
> "
UpdateOrderStatusAsync %
(% &
Guid& *
orderId+ 2
,2 3
string4 :
status; A
,A B
CancellationTokenC T
cancellationTokenU f
=g h
defaulti p
)p q
;q r
} ƒ	
hD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\DependencyInjection.cs
	namespace 	
Producao
 
. 
Application 
; 
public 
static 
class 
DependencyInjection '
{		 
public

 

static

 
IServiceCollection

 $
AddApplication

% 3
(

3 4
this

4 8
IServiceCollection

9 K
services

L T
)

T U
{ 
services 
. %
AddValidatorsFromAssembly *
(* +
Assembly+ 3
.3 4 
GetExecutingAssembly4 H
(H I
)I J
)J K
;K L
services 
. 

AddMediatR 
( 
cfg 
=>  "
{ 	
cfg 
. (
RegisterServicesFromAssembly ,
(, -
Assembly- 5
.5 6 
GetExecutingAssembly6 J
(J K
)K L
)L M
;M N
} 	
)	 

;
 
return 
services 
; 
} 
} ò#
}D:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Queries\GetProductionOrderByOrderIdQuery.cs
	namespace 	
Producao
 
. 
Application 
. 
Queries &
;& '
public 
class ,
 GetProductionOrderByOrderIdQuery -
:. /
IRequest0 8
<8 92
&GetProductionOrderByOrderIdQueryResult9 _
>_ `
{		 
public

 

Guid

 
OrderId

 
{

 
get

 
;

 
set

 "
;

" #
}

$ %
} 
public 
class 2
&GetProductionOrderByOrderIdQueryResult 3
{ 
public 

bool 
Success 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
? 
Error 
{ 
get 
; 
set  #
;# $
}% &
public 

ProductionOrderDto 
? 
Order $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
public 
class 
ProductionOrderDto 
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

Guid 
OrderId 
{ 
get 
; 
set "
;" #
}$ %
public 

Guid 
? 

CustomerId 
{ 
get !
;! "
set# &
;& '
}( )
public 

string 
? 
CustomerName 
{  !
get" %
;% &
set' *
;* +
}, -
public 

decimal 

TotalPrice 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
Status 
{ 
get 
; 
set  #
;# $
}% &
=' (
string) /
./ 0
Empty0 5
;5 6
public 

int 
EstimatedMinutes 
{  !
get" %
;% &
set' *
;* +
}, -
public 

int 
WaitingTimeMinutes !
{" #
get$ '
;' (
set) ,
;, -
}. /
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
public 

DateTime 
? 
	StartedAt 
{  
get! $
;$ %
set& )
;) *
}+ ,
public   

DateTime   
?   
ReadyAt   
{   
get   "
;  " #
set  $ '
;  ' (
}  ) *
public!! 

DateTime!! 
?!! 
CompletedAt!!  
{!!! "
get!!# &
;!!& '
set!!( +
;!!+ ,
}!!- .
public"" 

List"" 
<"" "
ProductionOrderItemDto"" &
>""& '
Items""( -
{"". /
get""0 3
;""3 4
set""5 8
;""8 9
}"": ;
=""< =
new""> A
(""A B
)""B C
;""C D
}## 
public%% 
class%% "
ProductionOrderItemDto%% #
{&& 
public'' 

Guid'' 
	ProductId'' 
{'' 
get'' 
;''  
set''! $
;''$ %
}''& '
public(( 

string(( 
ProductName(( 
{(( 
get((  #
;((# $
set((% (
;((( )
}((* +
=((, -
string((. 4
.((4 5
Empty((5 :
;((: ;
public)) 

int)) 
Quantity)) 
{)) 
get)) 
;)) 
set)) "
;))" #
}))$ %
public** 

decimal** 
	UnitPrice** 
{** 
get** "
;**" #
set**$ '
;**' (
}**) *
}++ ¶(
ÅD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Commands\CreateProductionOrderCommandHandler.cs
	namespace 	
Producao
 
. 
Application 
. 
Commands '
;' (
public

 
class

 /
#CreateProductionOrderCommandHandler

 0
:

1 2
IRequestHandler

3 B
<

B C(
CreateProductionOrderCommand

C _
,

_ `/
"CreateProductionOrderCommandResult	

a É
>


É Ñ
{ 
private 
readonly &
IProductionOrderRepository /
_repository0 ;
;; <
private 
readonly 
ILogger 
< /
#CreateProductionOrderCommandHandler @
>@ A
_loggerB I
;I J
public 
/
#CreateProductionOrderCommandHandler .
(. /&
IProductionOrderRepository "

repository# -
,- .
ILogger 
< /
#CreateProductionOrderCommandHandler 3
>3 4
logger5 ;
); <
{ 
_repository 
= 

repository  
;  !
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< .
"CreateProductionOrderCommandResult 8
>8 9
Handle: @
(@ A(
CreateProductionOrderCommandA ]
request^ e
,e f
CancellationTokeng x
cancellationToken	y ä
)
ä ã
{ 
_logger 
. 
LogInformation 
( 
$str R
,R S
requestT [
.[ \
OrderId\ c
)c d
;d e
var 
existing 
= 
await 
_repository (
.( )
GetByOrderIdAsync) :
(: ;
request; B
.B C
OrderIdC J
,J K
cancellationTokenL ]
)] ^
;^ _
if 

( 
existing 
!= 
null 
) 
{ 	
_logger 
. 

LogWarning 
( 
$str W
,W X
requestY `
.` a
OrderIda h
)h i
;i j
throw   
new   
ValidationException   )
(  ) *
$"  * ,
$str  , [
{  [ \
request  \ c
.  c d
OrderId  d k
}  k l
"  l m
)  m n
;  n o
}!! 	
var$$ 
items$$ 
=$$ 
request$$ 
.$$ 
Items$$ !
.$$! "
Select$$" (
($$( )
item$$) -
=>$$. 0
ProductionOrderItem%% 
.%%  
Create%%  &
(%%& '
item%%' +
.%%+ ,
	ProductId%%, 5
,%%5 6
item%%7 ;
.%%; <
ProductName%%< G
,%%G H
item%%I M
.%%M N
Quantity%%N V
,%%V W
item%%X \
.%%\ ]
	UnitPrice%%] f
)%%f g
)&& 	
.&&	 

ToList&&
 
(&& 
)&& 
;&& 
var)) 
productionOrder)) 
=)) 
ProductionOrder)) -
.))- .
Create)). 4
())4 5
request** 
.** 
OrderId** 
,** 
request++ 
.++ 

CustomerId++ 
,++ 
request,, 
.,, 
CustomerName,,  
,,,  !
request-- 
.-- 

TotalPrice-- 
,-- 
items.. 
)// 	
;//	 

await22 
_repository22 
.22 
CreateAsync22 %
(22% &
productionOrder22& 5
,225 6
cancellationToken227 H
)22H I
;22I J
_logger44 
.44 
LogInformation44 
(44 
$str44 d
,44d e
productionOrder55 
.55 
Id55 
,55 
request55  '
.55' (
OrderId55( /
)55/ 0
;550 1
return77 
new77 .
"CreateProductionOrderCommandResult77 5
{88 	
Id99 
=99 
productionOrder99  
.99  !
Id99! #
,99# $
OrderId:: 
=:: 
productionOrder:: %
.::% &
OrderId::& -
,::- .
Status;; 
=;; 
productionOrder;; $
.;;$ %
Status;;% +
.;;+ ,
ToString;;, 4
(;;4 5
);;5 6
,;;6 7
EstimatedMinutes<< 
=<< 
productionOrder<< .
.<<. /
EstimatedMinutes<</ ?
,<<? @
	CreatedAt== 
=== 
productionOrder== '
.==' (
	CreatedAt==( 1
}>> 	
;>>	 

}?? 
}@@ Œ+
ÑD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Queries\GetProductionOrderByOrderIdQueryHandler.cs
	namespace 	
Producao
 
. 
Application 
. 
Queries &
;& '
public 
class 3
'GetProductionOrderByOrderIdQueryHandler 4
:5 6
IRequestHandler7 F
<F G,
 GetProductionOrderByOrderIdQueryG g
,g h3
&GetProductionOrderByOrderIdQueryResult	i è
>
è ê
{		 
private

 
readonly

 &
IProductionOrderRepository

 /
_repository

0 ;
;

; <
private 
readonly 
ILogger 
< 3
'GetProductionOrderByOrderIdQueryHandler D
>D E
_loggerF M
;M N
public 
3
'GetProductionOrderByOrderIdQueryHandler 2
(2 3&
IProductionOrderRepository "

repository# -
,- .
ILogger 
< 3
'GetProductionOrderByOrderIdQueryHandler 7
>7 8
logger9 ?
)? @
{ 
_repository 
= 

repository  
;  !
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 2
&GetProductionOrderByOrderIdQueryResult <
>< =
Handle> D
(D E,
 GetProductionOrderByOrderIdQueryE e
requestf m
,m n
CancellationToken	o Ä
cancellationToken
Å í
)
í ì
{ 
_logger 
. 
LogInformation 
( 
$str S
,S T
requestU \
.\ ]
OrderId] d
)d e
;e f
var 
productionOrder 
= 
await #
_repository$ /
./ 0
GetByOrderIdAsync0 A
(A B
requestB I
.I J
OrderIdJ Q
,Q R
cancellationTokenS d
)d e
;e f
if 

( 
productionOrder 
== 
null #
)# $
{ 	
_logger 
. 

LogWarning 
( 
$str Y
,Y Z
request[ b
.b c
OrderIdc j
)j k
;k l
return 
new 2
&GetProductionOrderByOrderIdQueryResult =
{ 
Success   
=   
false   
,    
Error!! 
=!! 
$"!! 
$str!! I
{!!I J
request!!J Q
.!!Q R
OrderId!!R Y
}!!Y Z
"!!Z [
}"" 
;"" 
}## 	
return%% 
new%% 2
&GetProductionOrderByOrderIdQueryResult%% 9
{&& 	
Success'' 
='' 
true'' 
,'' 
Order(( 
=(( 
new(( 
ProductionOrderDto(( *
{)) 
Id** 
=** 
productionOrder** $
.**$ %
Id**% '
,**' (
OrderId++ 
=++ 
productionOrder++ )
.++) *
OrderId++* 1
,++1 2

CustomerId,, 
=,, 
productionOrder,, ,
.,,, -

CustomerId,,- 7
,,,7 8
CustomerName-- 
=-- 
productionOrder-- .
.--. /
CustomerName--/ ;
,--; <

TotalPrice.. 
=.. 
productionOrder.. ,
..., -

TotalPrice..- 7
,..7 8
Status// 
=// 
productionOrder// (
.//( )
Status//) /
./// 0
ToString//0 8
(//8 9
)//9 :
,//: ;
EstimatedMinutes00  
=00! "
productionOrder00# 2
.002 3
EstimatedMinutes003 C
,00C D
WaitingTimeMinutes11 "
=11# $
productionOrder11% 4
.114 5!
GetWaitingTimeMinutes115 J
(11J K
)11K L
,11L M
	CreatedAt22 
=22 
productionOrder22 +
.22+ ,
	CreatedAt22, 5
,225 6
	StartedAt33 
=33 
productionOrder33 +
.33+ ,
	StartedAt33, 5
,335 6
ReadyAt44 
=44 
productionOrder44 )
.44) *
ReadyAt44* 1
,441 2
CompletedAt55 
=55 
productionOrder55 -
.55- .
CompletedAt55. 9
,559 :
Items66 
=66 
productionOrder66 '
.66' (
Items66( -
.66- .
Select66. 4
(664 5
item665 9
=>66: <
new66= @"
ProductionOrderItemDto66A W
{77 
	ProductId88 
=88 
item88  $
.88$ %
	ProductId88% .
,88. /
ProductName99 
=99  !
item99" &
.99& '
ProductName99' 2
,992 3
Quantity:: 
=:: 
item:: #
.::# $
Quantity::$ ,
,::, -
	UnitPrice;; 
=;; 
item;;  $
.;;$ %
	UnitPrice;;% .
}<< 
)<< 
.<< 
ToList<< 
(<< 
)<< 
}== 
}>> 	
;>>	 

}?? 
}@@ “
xD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Common\Exceptions\NotFoundException.cs
	namespace 	
Producao
 
. 
Application 
. 
Common %
.% &

Exceptions& 0
;0 1
public 
class 
NotFoundException 
:  
	Exception! *
{ 
public 

NotFoundException 
( 
string #
message$ +
)+ ,
:- .
base/ 3
(3 4
message4 ;
); <
{ 
} 
public		 

NotFoundException		 
(		 
string		 #
name		$ (
,		( )
object		* 0
key		1 4
)		4 5
:

 	
base


 
(

 
$"

 
$str

 
{

 
name

 !
}

! "
$str

" &
{

& '
key

' *
}

* +
$str

+ @
"

@ A
)

A B
{ 
} 
} ©?
áD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Commands\UpdateProductionOrderStatusCommandHandler.cs
	namespace		 	
Producao		
 
.		 
Application		 
.		 
Commands		 '
;		' (
public 
class 5
)UpdateProductionOrderStatusCommandHandler 6
:7 8
IRequestHandler9 H
<H I.
"UpdateProductionOrderStatusCommandI k
,k l5
(UpdateProductionOrderStatusCommandResult	m ï
>
ï ñ
{ 
private 
readonly &
IProductionOrderRepository /
_repository0 ;
;; <
private 
readonly  
INotificationService ) 
_notificationService* >
;> ?
private 
readonly 
IOrderServiceClient (
_orderServiceClient) <
;< =
private 
readonly 
ILogger 
< 5
)UpdateProductionOrderStatusCommandHandler F
>F G
_loggerH O
;O P
public 
5
)UpdateProductionOrderStatusCommandHandler 4
(4 5&
IProductionOrderRepository "

repository# -
,- . 
INotificationService 
notificationService 0
,0 1
IOrderServiceClient 
orderServiceClient .
,. /
ILogger 
< 5
)UpdateProductionOrderStatusCommandHandler 9
>9 :
logger; A
)A B
{ 
_repository 
= 

repository  
;  ! 
_notificationService 
= 
notificationService 2
;2 3
_orderServiceClient 
= 
orderServiceClient 0
;0 1
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< 4
(UpdateProductionOrderStatusCommandResult >
>> ?
Handle@ F
(F G.
"UpdateProductionOrderStatusCommandG i
requestj q
,q r
CancellationToken	s Ñ
cancellationToken
Ö ñ
)
ñ ó
{ 
_logger   
.   
LogInformation   
(   
$str   a
,  a b
request  c j
.  j k
OrderId  k r
,  r s
request  t {
.  { |
Status	  | Ç
)
  Ç É
;
  É Ñ
var"" 
productionOrder"" 
="" 
await"" #
_repository""$ /
.""/ 0
GetByOrderIdAsync""0 A
(""A B
request""B I
.""I J
OrderId""J Q
,""Q R
cancellationToken""S d
)""d e
;""e f
if## 

(## 
productionOrder## 
==## 
null## #
)### $
throw$$ 
new$$ 
NotFoundException$$ '
($$' (
$"$$( *
$str$$* Y
{$$Y Z
request$$Z a
.$$a b
OrderId$$b i
}$$i j
"$$j k
)$$k l
;$$l m
if&& 

(&& 
!&& 
Enum&& 
.&& 
TryParse&& 
<&& 
ProductionStatus&& +
>&&+ ,
(&&, -
request&&- 4
.&&4 5
Status&&5 ;
,&&; <
true&&= A
,&&A B
out&&C F
var&&G J
	newStatus&&K T
)&&T U
)&&U V
throw'' 
new'' 
ValidationException'' )
('') *
$"''* ,
$str'', =
{''= >
request''> E
.''E F
Status''F L
}''L M
"''M N
)''N O
;''O P
var)) 
previousStatus)) 
=)) 
productionOrder)) ,
.)), -
Status))- 3
.))3 4
ToString))4 <
())< =
)))= >
;))> ?
productionOrder,, 
.,, 
UpdateStatus,, $
(,,$ %
	newStatus,,% .
),,. /
;,,/ 0
await-- 
_repository-- 
.-- 
UpdateAsync-- %
(--% &
productionOrder--& 5
,--5 6
cancellationToken--7 H
)--H I
;--I J
await00  
_notificationService00 "
.00" #(
NotifyOrderStatusChangeAsync00# ?
(00? @
productionOrder11 
.11 
OrderId11 #
,11# $
previousStatus22 
,22 
	newStatus33 
.33 
ToString33 
(33 
)33  
,33  !
productionOrder44 
.44 

CustomerId44 &
)55 	
;55	 

if88 

(88 
	newStatus88 
==88 
ProductionStatus88 )
.88) *
Ready88* /
)88/ 0
{99 	
_logger:: 
.:: 
LogInformation:: "
(::" #
$str::# `
,::` a
productionOrder::b q
.::q r
OrderId::r y
)::y z
;::z {
await;;  
_notificationService;; &
.;;& '!
NotifyOrderReadyAsync;;' <
(;;< =
productionOrder;;= L
.;;L M
OrderId;;M T
,;;T U
productionOrder;;V e
.;;e f

CustomerId;;f p
);;p q
;;;q r
}<< 	
if?? 

(?? 
	newStatus?? 
==?? 
ProductionStatus?? )
.??) *
Ready??* /
||??0 2
	newStatus??3 <
==??= ?
ProductionStatus??@ P
.??P Q
	Completed??Q Z
)??Z [
{@@ 	
tryAA 
{BB 
varCC 
orderStatusCC 
=CC  !
	newStatusCC" +
==CC, .
ProductionStatusCC/ ?
.CC? @
ReadyCC@ E
?CCF G
$strCCH O
:CCP Q
$strCCR ]
;CC] ^
awaitDD 
_orderServiceClientDD )
.DD) *"
UpdateOrderStatusAsyncDD* @
(DD@ A
productionOrderDDA P
.DDP Q
OrderIdDDQ X
,DDX Y
orderStatusDDZ e
,DDe f
cancellationTokenDDg x
)DDx y
;DDy z
_loggerEE 
.EE 
LogInformationEE &
(EE& '
$strEE' t
,EEt u
productionOrderFF #
.FF# $
OrderIdFF$ +
,FF+ ,
orderStatusFF- 8
)FF8 9
;FF9 :
}GG 
catchHH 
(HH 
	ExceptionHH 
exHH 
)HH  
{II 
_loggerJJ 
.JJ 
LogErrorJJ  
(JJ  !
exJJ! #
,JJ# $
$strJJ% d
,JJd e
productionOrderJJf u
.JJu v
OrderIdJJv }
)JJ} ~
;JJ~ 
}LL 
}MM 	
_loggerOO 
.OO 
LogInformationOO 
(OO 
$strOO `
,OO` a
requestOOb i
.OOi j
OrderIdOOj q
,OOq r
	newStatusOOs |
)OO| }
;OO} ~
returnQQ 
newQQ 4
(UpdateProductionOrderStatusCommandResultQQ ;
{RR 	
IdSS 
=SS 
productionOrderSS  
.SS  !
IdSS! #
,SS# $
OrderIdTT 
=TT 
productionOrderTT %
.TT% &
OrderIdTT& -
,TT- .
StatusUU 
=UU 
productionOrderUU $
.UU$ %
StatusUU% +
.UU+ ,
ToStringUU, 4
(UU4 5
)UU5 6
,UU6 7
	UpdatedAtVV 
=VV 
productionOrderVV '
.VV' (
	UpdatedAtVV( 1
??VV2 4
DateTimeVV5 =
.VV= >
UtcNowVV> D
,VVD E
NotificationSentWW 
=WW 
trueWW #
}XX 	
;XX	 

}YY 
}ZZ  
ÄD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Commands\UpdateProductionOrderStatusCommand.cs
	namespace 	
Producao
 
. 
Application 
. 
Commands '
;' (
public 
class .
"UpdateProductionOrderStatusCommand /
:0 1
IRequest2 :
<: ;4
(UpdateProductionOrderStatusCommandResult; c
>c d
{		 
public

 

Guid

 
OrderId

 
{

 
get

 
;

 
set

 "
;

" #
}

$ %
public 

string 
Status 
{ 
get 
; 
set  #
;# $
}% &
=' (
string) /
./ 0
Empty0 5
;5 6
} 
public 
class 4
(UpdateProductionOrderStatusCommandResult 5
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

Guid 
OrderId 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
Status 
{ 
get 
; 
set  #
;# $
}% &
=' (
string) /
./ 0
Empty0 5
;5 6
public 

DateTime 
	UpdatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
public 

bool 
NotificationSent  
{! "
get# &
;& '
set( +
;+ ,
}- .
} ¶-
~D:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Queries\GetOrdersInProductionQueryHandler.cs
	namespace 	
Producao
 
. 
Application 
. 
Queries &
;& '
public 
class -
!GetOrdersInProductionQueryHandler .
:/ 0
IRequestHandler1 @
<@ A&
GetOrdersInProductionQueryA [
,[ \,
 GetOrdersInProductionQueryResult] }
>} ~
{		 
private

 
readonly

 &
IProductionOrderRepository

 /
_repository

0 ;
;

; <
private 
readonly 
ILogger 
< -
!GetOrdersInProductionQueryHandler >
>> ?
_logger@ G
;G H
public 
-
!GetOrdersInProductionQueryHandler ,
(, -&
IProductionOrderRepository "

repository# -
,- .
ILogger 
< -
!GetOrdersInProductionQueryHandler 1
>1 2
logger3 9
)9 :
{ 
_repository 
= 

repository  
;  !
_logger 
= 
logger 
; 
} 
public 

async 
Task 
< ,
 GetOrdersInProductionQueryResult 6
>6 7
Handle8 >
(> ?&
GetOrdersInProductionQuery? Y
requestZ a
,a b
CancellationTokenc t
cancellationToken	u Ü
)
Ü á
{ 
_logger 
. 
LogInformation 
( 
$str _
,_ `
requesta h
.h i
Statusi o
,o p
requestq x
.x y

PageNumber	y É
)
É Ñ
;
Ñ Ö
IEnumerable 
< 
Domain 
. 
ProductionOrders +
.+ ,
Entities, 4
.4 5
ProductionOrder5 D
>D E
ordersF L
;L M
if 

( 
! 
string 
. 
IsNullOrWhiteSpace &
(& '
request' .
.. /
Status/ 5
)5 6
&&7 9
Enum 
. 
TryParse 
< 
ProductionStatus *
>* +
(+ ,
request, 3
.3 4
Status4 :
,: ;
true< @
,@ A
outB E
varF I
statusJ P
)P Q
)Q R
{ 	
orders 
= 
await 
_repository &
.& '
GetByStatusAsync' 7
(7 8
status8 >
,> ?
request@ G
.G H

PageNumberH R
,R S
requestT [
.[ \
PageSize\ d
,d e
cancellationTokenf w
)w x
;x y
} 	
else   
{!! 	
orders"" 
="" 
await"" 
_repository"" &
.""& '&
GetOrdersInProductionAsync""' A
(""A B
request""B I
.""I J

PageNumber""J T
,""T U
request""V ]
.""] ^
PageSize""^ f
,""f g
cancellationToken""h y
)""y z
;""z {
}## 	
var%% 

orderItems%% 
=%% 
orders%% 
.%%  
Select%%  &
(%%& '
o%%' (
=>%%) +
new%%, /
ProductionOrderItem%%0 C
{&& 	
Id'' 
='' 
o'' 
.'' 
Id'' 
,'' 
OrderId(( 
=(( 
o(( 
.(( 
OrderId(( 
,((  

CustomerId)) 
=)) 
o)) 
.)) 

CustomerId)) %
,))% &
CustomerName** 
=** 
o** 
.** 
CustomerName** )
,**) *

TotalPrice++ 
=++ 
o++ 
.++ 

TotalPrice++ %
,++% &
Status,, 
=,, 
o,, 
.,, 
Status,, 
.,, 
ToString,, &
(,,& '
),,' (
,,,( )
EstimatedMinutes-- 
=-- 
o--  
.--  !
EstimatedMinutes--! 1
,--1 2
WaitingTimeMinutes.. 
=..  
o..! "
..." #!
GetWaitingTimeMinutes..# 8
(..8 9
)..9 :
,..: ;
	CreatedAt// 
=// 
o// 
.// 
	CreatedAt// #
,//# $
	StartedAt00 
=00 
o00 
.00 
	StartedAt00 #
,00# $
ReadyAt11 
=11 
o11 
.11 
ReadyAt11 
,11  

ItemsCount22 
=22 
o22 
.22 
Items22  
.22  !
Count22! &
}33 	
)33	 

.33
 
ToList33 
(33 
)33 
;33 
return55 
new55 ,
 GetOrdersInProductionQueryResult55 3
{66 	
Orders77 
=77 

orderItems77 
,77  

PageNumber88 
=88 
request88  
.88  !

PageNumber88! +
,88+ ,
PageSize99 
=99 
request99 
.99 
PageSize99 '
,99' (

TotalCount:: 
=:: 

orderItems:: #
.::# $
Count::$ )
};; 	
;;;	 

}<< 
}== é"
wD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Queries\GetOrdersInProductionQuery.cs
	namespace 	
Producao
 
. 
Application 
. 
Queries &
;& '
public 
class &
GetOrdersInProductionQuery '
:( )
IRequest* 2
<2 3,
 GetOrdersInProductionQueryResult3 S
>S T
{		 
public

 

int

 

PageNumber

 
{

 
get

 
;

  
set

! $
;

$ %
}

& '
=

( )
$num

* +
;

+ ,
public 

int 
PageSize 
{ 
get 
; 
set "
;" #
}$ %
=& '
$num( *
;* +
public 

string 
? 
Status 
{ 
get 
;  
set! $
;$ %
}& '
} 
public 
class ,
 GetOrdersInProductionQueryResult -
{ 
public 

List 
< 
ProductionOrderItem #
># $
Orders% +
{, -
get. 1
;1 2
set3 6
;6 7
}8 9
=: ;
new< ?
(? @
)@ A
;A B
public 

int 

PageNumber 
{ 
get 
;  
set! $
;$ %
}& '
public 

int 
PageSize 
{ 
get 
; 
set "
;" #
}$ %
public 

int 

TotalCount 
{ 
get 
;  
set! $
;$ %
}& '
public 

int 

TotalPages 
=> 
( 
int !
)! "
Math" &
.& '
Ceiling' .
(. /

TotalCount/ 9
/: ;
(< =
double= C
)C D
PageSizeD L
)L M
;M N
} 
public 
class 
ProductionOrderItem  
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

Guid 
OrderId 
{ 
get 
; 
set "
;" #
}$ %
public 

Guid 
? 

CustomerId 
{ 
get !
;! "
set# &
;& '
}( )
public 

string 
? 
CustomerName 
{  !
get" %
;% &
set' *
;* +
}, -
public 

decimal 

TotalPrice 
{ 
get  #
;# $
set% (
;( )
}* +
public 

string 
Status 
{ 
get 
; 
set  #
;# $
}% &
=' (
string) /
./ 0
Empty0 5
;5 6
public   

int   
EstimatedMinutes   
{    !
get  " %
;  % &
set  ' *
;  * +
}  , -
public!! 

int!! 
WaitingTimeMinutes!! !
{!!" #
get!!$ '
;!!' (
set!!) ,
;!!, -
}!!. /
public"" 

DateTime"" 
	CreatedAt"" 
{"" 
get""  #
;""# $
set""% (
;""( )
}""* +
public## 

DateTime## 
?## 
	StartedAt## 
{##  
get##! $
;##$ %
set##& )
;##) *
}##+ ,
public$$ 

DateTime$$ 
?$$ 
ReadyAt$$ 
{$$ 
get$$ "
;$$" #
set$$$ '
;$$' (
}$$) *
public%% 

int%% 

ItemsCount%% 
{%% 
get%% 
;%%  
set%%! $
;%%$ %
}%%& '
}&& ˜
zD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Common\Exceptions\ValidationException.cs
	namespace 	
Producao
 
. 
Application 
. 
Common %
.% &

Exceptions& 0
;0 1
public 
class 
ValidationException  
:! "
	Exception# ,
{ 
public 

ValidationException 
( 
string %
message& -
)- .
:/ 0
base1 5
(5 6
message6 =
)= >
{ 
} 
public		 

ValidationException		 
(		 
string		 %
message		& -
,		- .
	Exception		/ 8
innerException		9 G
)		G H
:

 	
base


 
(

 
message

 
,

 
innerException

 &
)

& '
{ 
} 
} ù
zD:\Projetos\techChallenge - Fase 4\MicroServico-Producao\src\Producao.Application\Commands\CreateProductionOrderCommand.cs
	namespace 	
Producao
 
. 
Application 
. 
Commands '
;' (
public 
class (
CreateProductionOrderCommand )
:* +
IRequest, 4
<4 5.
"CreateProductionOrderCommandResult5 W
>W X
{		 
public

 

Guid

 
OrderId

 
{

 
get

 
;

 
set

 "
;

" #
}

$ %
public 

Guid 
? 

CustomerId 
{ 
get !
;! "
set# &
;& '
}( )
public 

string 
? 
CustomerName 
{  !
get" %
;% &
set' *
;* +
}, -
public 

decimal 

TotalPrice 
{ 
get  #
;# $
set% (
;( )
}* +
public 

List 
< ,
 CreateProductionOrderItemCommand 0
>0 1
Items2 7
{8 9
get: =
;= >
set? B
;B C
}D E
=F G
newH K
(K L
)L M
;M N
} 
public 
class ,
 CreateProductionOrderItemCommand -
{ 
public 

Guid 
	ProductId 
{ 
get 
;  
set! $
;$ %
}& '
public 

string 
ProductName 
{ 
get  #
;# $
set% (
;( )
}* +
=, -
string. 4
.4 5
Empty5 :
;: ;
public 

int 
Quantity 
{ 
get 
; 
set "
;" #
}$ %
public 

decimal 
	UnitPrice 
{ 
get "
;" #
set$ '
;' (
}) *
} 
public 
class .
"CreateProductionOrderCommandResult /
{ 
public 

Guid 
Id 
{ 
get 
; 
set 
; 
}  
public 

Guid 
OrderId 
{ 
get 
; 
set "
;" #
}$ %
public 

string 
Status 
{ 
get 
; 
set  #
;# $
}% &
=' (
string) /
./ 0
Empty0 5
;5 6
public 

int 
EstimatedMinutes 
{  !
get" %
;% &
set' *
;* +
}, -
public 

DateTime 
	CreatedAt 
{ 
get  #
;# $
set% (
;( )
}* +
}   
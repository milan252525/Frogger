# PRG 2 - Hra Frogger

## Milan Abrahám

## Zadání

Cílem bylo vytvořit vlastní verzi hry z konzole ZX Spectrum, kde se současně pohybuje více objektů. 

## Úvod

Hrou, kterou jsem se rozhodl napodobit, je Frogger z roku 1992 [[1]](https://youtu.be/s8jgU0ehfho). Inspirací mi byla ještě jiná stejnojmenná verze hry z roku 1981 [2] a to hlavně její lepším grafickým zpracování. Hra je vytvořená v jazyce C# pomocí grafické knihovny Windows Forms. 

Hráč ovládá žábu, které musí pomoci dostat se na druhou stranu herní obrazovky (na lekníny). V cestě jí ovšem překáží silnice, na kterých jezdí vozidla, nebo řeky po nichž se plaví klády. Žába ovšem neumí plavat, proto je nutné nespadnout do vody a přeskákat po kládách. Hra obsahuje několik úrovní s rozdílnou obtížností, včetně náhodně generované. Hra má jednoduché ovládání, žába se pohybuje pomocí šipek (nahoru, doleva, doprava, dolů). Hráč má 5 životů/žab a 3 musí dostat na lekníny, aby vyhrál.

## Herní objekty

Rozhodl jsem se pro hru nakreslit vlastní obrázky formou pixel artu, kde jsou vybarvovány jednotlivé pixely. Všechny mají velikost 64x64 pixelů, stejně jako jeden dílek herního prostředí. 

K nakraslení všech prvků jsem použil webovou stránku Pixilart [3].

Pozadí je pak skládáno z jednotlivých čtverců, stejně tak jako kláda, která má více částí.

| Obrázek                                                                                                                        | Název   | Popis                                                                                    |
| ------------------------------------------------------------------------------------------------------------------------------ |:-------:| ---------------------------------------------------------------------------------------- |
| ![](https://github.com/milan252525/Frogger/blob/master/Frogger/Resources/frog.png?raw=true)                                    | žába    | Hlavní objekt ovládaný hráčem.                                                           |
| ![](https://github.com/milan252525/Frogger/blob/master/Frogger/Resources/lilypad.png?raw=true)                                 | leknín  | Cílem hry je dostat všechny žáby.                                                        |
| <img title="" src="https://raw.githubusercontent.com/milan252525/Frogger/master/Frogger/Resources/car_yellow_left.png" alt=""> | auto    | Pohybuje se po silnici, žába se s ním nesmí střetnout jinak zemře. Má více možnýc barev. |
| <img title="" src="https://github.com/milan252525/Frogger/blob/master/Frogger/Resources/river.png?raw=true" alt="">            | řeka    | Žába nesmí do řeky spadnout, neumí plavat a utopí se.                                    |
| <img title="" src="https://github.com/milan252525/Frogger/blob/master/Frogger/Resources/log_left.png?raw=true" alt="">         | kláda   | Kláda plave po řece, žába na ní může skočit, ale pohybuje se spolu s ní.                 |
| <img title="" src="https://github.com/milan252525/Frogger/blob/master/Frogger/Resources/grass.png?raw=true" alt="">            | tráva   | Tráva je pro žábu bezpečná.                                                              |
| <img title="" src="https://github.com/milan252525/Frogger/blob/master/Frogger/Resources/beach.png?raw=true" alt="">            | pláž    | Tráva je pro žábu bezpečná.                                                              |
| <img title="" src="https://github.com/milan252525/Frogger/blob/master/Frogger/Resources/road.png?raw=true" alt="">             | silnice | Silnice je pro žábu bezpečná, ale musí dávat pozor na přijíždějící auta.                 |

## Hlavní menu



## Herní prostředí

Prostředí je tvořené z dílku, každý má velikost 64x64 bodů, právě kvůli velikosti obrázků. Nejdřív je nakresleno pozadí, podle přednastavených pravidel. Na pozadí jsou potom přikreslovány herní objetky.

Prostředí se překresluje každých 130 ms. Objekty se poté pohybují každé čtvrté překreslení (cca půl vteřiny), kromě žáby, která se může pohnout každé překreslení. Uživatel ovšem není schopen pohybovat žábou tak rychle. Žába se, tak může pohybovat z důvodu snadnějšího uhýbání jiným objektům.

Objekty včetně pozadí nejsou vykreslovány přímo na obrazovku. Nakreslí se nejdříve na skrytou bitmapu, která je potom zobrazena na obrazovku. Důvodem tohoto řešení je, že vykreslování přímo na obrazovku způsobovalo nepříjemné blikání jednotlivých objektů.

## Objektový návrh

Objektový návrh je znázorněn na následujícím UML diagramu. Všechny objekty dědí od abstraktní třídy GameObject. Mají metodu draw, která umožňuje jejich nakreslení na herní plochu. Objekty, kterými je třeba pohybovat, jsou potomky třídy MovingGameObject, mají navíc metodu, která jimi pohne. Celé pozadí je jedním objektem, vždy se nakreslí první a vykreslí celé pozadí podle předem daných pravidel, která jsou uložená v objektu Game.

Game je hlavní objekt obsahující všechna data o hře, uchovává všechny objekty ve dvou polích, jedno s pohyblivými objekty a druhé se statickými. Dále obsahuje pravidla pro strukturu herního prostředí a vytváření nových objektů, udržuje stav hry, skóre atd. 

<div>
<img src="https://github.com/milan252525/Frogger/blob/master/docs/uml_frogger.png?raw=true">
</div>

## Herní úrovně

Hra obsahuje 3 rozdílně obtížné úrovně s různými prostředí a možnost nechat si prostředí náhodně vygenerovat.

## Zdroje

[1] [Frogger (Deanysoft) Walkthrough, ZX Spectrum - YouTube](https://youtu.be/s8jgU0ehfho)

[2] [Arcade Game: Frogger (1981 Konami) - YouTube](https://youtu.be/WNrz9_Fe-Us)

[3] [Pixilart - Free Online Art Community and Pixel Art Tool](https://www.pixilart.com/)



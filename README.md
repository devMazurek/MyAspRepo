# MyAspRepo
POL
Zawartością niniejszego repozytorium jest aplikacja webowa implementująca jeden z najpopularniejszych algorytmów wspomagania decyzji, czyli
Analitic Hierarchy Process (AHP). Algorytm oraz model AHP służy ustrukturyzowaniu obranych kryteriów decyzyjnych, a następnie ich ocenę
w odniesieniu do wyznaczonych alternatyw. 

Najprościej model ten można przedstawić za pomocą następujących poziomów:
1. Cel - nadrzędny motyw podejmowania decyzji (np. kupno samochodu)
2. Alternatywy - wstępnie wybrane propozycje zgdone obranym celem (np. Honda Civic, VW Golf)
3. Kryteria - czynniki, którymi będziemy się kierować przy wyborze alternatywy (np. wyposażenie, cena, przzestronność)
4. Dalsze podpoziomy poszczególnych kryteriów, np. subkryteria (np. dla wyposażenia: klimatyzacja, nagłośnienie, fotele z fukcją masażu)

Użytkownik ocenia parami kryteria (każdy z każdym) w 9 - stopniowej skali Saat'yego, gdzie ocena 1 to porównywalna ważnośći obu kryteriów a 9
to silna przewa jednego kryterium nad drugim.
W przykładowym modelu czterostopniowym (cel, alternatywy, kryteria, subkryteria) należy wykonać następujące porównania:
1. Ocena wszystkich kryteriów ze sobą w odniesieniu do celu decyzyjnego
2. Ocena wszystkich subkryteriów ze sobą w odniesieniu do nadrzednego kryteium
3. Ocena wszystkich alternatyw ze sobą w odniesieniu do każdego z kryterium 
4. Ocena wszystkich alternatyw ze sobą w odniesieniu do każdego z subkryterium.

Można zakończyć ocenianie na etapie 3. jednak bardziej prawidłowy i dokładny wynik decyzyjny osiągniemy analizując oceny na najniższym,
czyli na 4. poziomie

Na każdym etapie wyliczane są tzw. priorytety lokalne i globalne (ważniejsze), w efekcie czego otrzymujemy stosowną klasyfikację wszystkich
wytypowanych na początku alternatyw.

ENG
The content of this repository is a web application that implements one of the most popular decision making algorithms.
Analitic Hierarchy Process (AHP). The Algorithm and AHP model are used to structure the selected decision criteria and then evaluate them
With respect to the alternatives.

The easiest way to do this is by using the following levels:
1. Objective|GOL - overriding motive for decision making (eg car purchase)
2. Alternatives - Pre-selected proposals for the chosen target (eg Honda Civic, VW Golf)
3. Criteria - the factors we will guide when choosing an alternative (eg equipment, price, generosity)
4. Subsequent sub-criteria, such as sub-criteria (eg: air conditioning, sound system, massage chairs)

The user evaluates the criteria (each with each) in a 9-step scale Saat, where rating 1 is comparable to both criteria and 9
This is strong over one criterion over another.
In the of the four-stage model (target, alternatives, criteria, sub-criteria) the following comparisons should be made:
1. Evaluate all criteria with each other in relation to the decision goal
2. Evaluate all subtypes with each other in relation to the upper convention
3. Evaluate all alternatives with each other for each criterion
4. Evaluate all alternatives with each other for each sub-criterion.

You can finish the assessment at stage 3. However, a more accurate decision result will be obtained by analyzing the scores on the lowest,
That is on level 4

At each stage are calculated so-called local and global priorities (more important), resulting in an appropriate classification of all
Selected early alternatives.

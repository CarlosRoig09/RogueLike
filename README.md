# M17UF2R2-RoigGarciaCarlos

# Introduccion

En aquest document mostrare el concepte i mecaniques del meu joc RogueLike ara en una fase molt temprana del desenvolupament. 

## Sentit del meu joc

Al final he aconseguit fer el que vaig proposar. He fet un ventall molt ampli de armes fisiques. Si be m'ha faltat apartat estetic i sonor (No tinc efectes de so ni tampoc el menu per editar-hos) he aconseguit un joc procedural amb la mecaniques que buscaba.
El trets dels enemics es poden retornar amb qualsevol atac de qualsevol arma (menys la flecha de l'arc) i totes les armes tenen atac melee i atac a distancia.
M'hagues agradat fer el patro state a les weapons, pero he aconseguit fer una interficie on es pot definir qualsevol weapon de dos atacs. 
El sistema procedural estaba pensat per a ser en una matriu, pero per falta de temps es una array.
## Instruccions

Disparar - LeftClick.
MeleeAttack - RightClick
ChangeWeapon - Scroll
Dash - LeftShift
Movement - WASD or Arrows
Q - Throw Bomb (Les bombes eren un item recollible. Per falta de temps nomes comencesamb 5).

## Millores 
Les weapons tenen melee attack.
Pots retornar els trets.
Hi ha puntuacio ben implementada.
Joc mes equilibrat
Dash + attac implementat
Millors escenaris
Kamikazee enemy si sembla mes una explosio
Turret es pot teleporta com a patro
Patro state implementat

## Bug No Arreclat Advertencia!!!
Al entrar a la tenda per primer cop, ets teleportat a l'anterior sala. Al retornar a la tenda tot torna a la normalitat.
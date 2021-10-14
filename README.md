# *Promotion Engine*

<br/>

## **Promotion Engine - Console Application**

JOB INTERVIEW - CODE CHALLANGE 

The promotion rules are mutually exclusive, that implies only one promotion (individual SKU or combined) is applicable to a particular SKU. Rest depends on the imagination of the programmer for which scenarios they want to consider, for example (case 1 => 2A = 30 and A=A40%) or (case 2 => either 2A = 30 or A=A40%)

Problem Statement 1: Promotion Engine

We need you to implement a simple promotion engine for a checkout process. Our Cart contains a list of single character SKU ids (A, B, C. ..) over which the promotion engine will need to run.

The promotion engine will need to calculate the total order value after applying the 2 promotion types

• buy 'n' items of a SKU for a fixed price (3 A's for 130)
• buy SKU 1 & SKU 2 for a fixed price ( C + D = 30 )

The promotion engine should be modular to allow for more promotion types to be added at a later date (e.g. a future promotion could be x% of a SKU unit price). For this coding exercise you can assume that the promotions will be mutually exclusive; in other words if one is applied the other promotions will not apply

Test Setup <br>
Unit price for SKU IDs<br> 
A 50<br>
B 30<br>
C 20<br>
D 15<br>

Active Promotions<br>
3 of A's for 130<br>
2 of B's for 45<br> 
C & D for 30<br>

Scenario A<br>
1 * A = 50<br>
1 * B = 30<br>
1 * C = 20<br>
Total 100<br>

Scenario B<br>
5 * A = 130 + 2*50<br>
5 * B = 45 + 45 + 30<br>
1 * C = 28<br>
Total 370<br>

Scenario C<br>
3 * A = 130<br>
5 * B = 45 + 45 + 1 * 30<br>
1 * C = -<br>
1 * D = 30<br>
Total 280

<hr>

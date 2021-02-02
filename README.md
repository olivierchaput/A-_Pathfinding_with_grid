<h1 align="center">README - A* Pathfinding with grid </h1>
<p align="center"><i>Contains all scripts required to build a basic A* pathfinding algorithm. Non-optimized.
Works by checking all neighbouring nodes including diagonals.</i></p>
<div align="center">
</div>
<br>
<br>



How to use :
- Create an empty gameObject for the grid and assign it the grid script
  - Enter grid dimensions, cell-size, start position, obstacle mask and distance between cells
- Create an empty gameobject for the pathfinding and assign it pathfinding script
  - Add reference to grid script, start position, end position


To do : 
- Add selector to enable or disable gizmos
- Code optimisations
  - Reduce number of lines for the 4 neighbour checking
  - Modify grid without having to generate a new one each time an object is placed
- Add simple function to call to redraw grid and to refind best path




# Contribute

TBD

## :man_astronaut: Show your support

Give a ⭐️ if this project helped you!

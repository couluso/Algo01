using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private GameObject Sol;

    [SerializeField]
    private GameObject Plafond;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject Camera;

    [SerializeField]
    private GameObject Orbe;

    [SerializeField]
    private int _mazeDepth;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int compteur_orbe = 0;

    [SerializeField]
    private int _nombreOuvertures = 10;

    [SerializeField]
    private bool Délai;


    private List<MazeCell> _interiorCells = new List<MazeCell>();

    private MazeCell[,] _mazeGrid;

    //generation de la grille
    IEnumerator Start()
    {
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];


        for (int x = 0; x < _mazeWidth; x += 3)
        {
            for (int z = 0; z < _mazeDepth; z += 3)
            {
                MazeCell cell = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                _mazeGrid[x, z] = cell;

                // On ajoute uniquement les cellules intérieures.
                // Les cellules de la bordure ne pourront donc
                // jamais être sélectionnées pour créer une ouverture.
                if (x > 0 && x < _mazeWidth - 3 && z > 0 && z < _mazeDepth - 3)
                {
                    _interiorCells.Add(cell);
                }

                if (Random.Range(0, 10) != 0)
                {
                    Destroy(_mazeGrid[x, z]._light);
                }
            }
        }

        yield return GenerateMaze(null, _mazeGrid[0, 0]); //debut du checkup
        
        CreateRandomOpenings();
        PlacerOrbes();






        GameObject sol = Instantiate(Sol);
        Camera.SetActive(false);
        sol.transform.localScale = new Vector3(_mazeWidth / 10, 1, _mazeDepth / 10);
        GameObject plafond = Instantiate(Plafond);
        plafond.transform.localScale = new Vector3(_mazeWidth, 1, _mazeDepth);
        Instantiate(Player, new Vector3(0, 1, 0), Quaternion.identity);

    }

    //fonction recurrente
    private IEnumerator GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);


        if (Délai == true)
        {
            yield return new WaitForSeconds(0.005f);
        }


        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                yield return GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }

    //se deplace vers les cellules non visite
    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = (int)currentCell.transform.position.x;
        int z = (int)currentCell.transform.position.z;

        if (x + 3 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 3, z];

            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }

        if (x - 3 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 3, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 3 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 3];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }

        if (z - 3 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 3];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
        Debug.Log(x + " chemin " + z);
    }

    //retire les murs
    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }

        if (previousCell.transform.position.x < currentCell.transform.position.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
            return;
        }

        if (previousCell.transform.position.x > currentCell.transform.position.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }

        if (previousCell.transform.position.z < currentCell.transform.position.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
            return;
        }

        if (previousCell.transform.position.z > currentCell.transform.position.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }
    }

    private void PlacerOrbes()
    {
        // Récupère toutes les cellules existantes de la grille
        List<MazeCell> cellulesDisponibles = new List<MazeCell>();
        for (int x = 0; x < _mazeWidth; x += 3)
        {
            for (int z = 0; z < _mazeDepth; z += 3)
            {
                if (_mazeGrid[x, z] != null)
                {
                    cellulesDisponibles.Add(_mazeGrid[x, z]);
                }
            }
        }

        // Exclut la cellule de départ (0,0) où apparaît le joueur
        cellulesDisponibles.RemoveAll(c =>
            (int)c.transform.position.x == 0 && (int)c.transform.position.z == 0);

        // Mélange la liste
        cellulesDisponibles = cellulesDisponibles.OrderBy(_ => Random.value).ToList();

        // Sécurité si le labyrinthe est trop petit pour 10 orbes
        int nombreAPlacer = Mathf.Min(compteur_orbe, cellulesDisponibles.Count);

        for (int i = 0; i < nombreAPlacer; i++)
        {
            Vector3 position = cellulesDisponibles[i].transform.position;
            position.y = 1f; // ajustez la hauteur selon votre prefab Orbe
            Instantiate(Orbe, position, Quaternion.identity);
        }

        compteur_orbe = nombreAPlacer;
    }

    // ============================================================
    // CREATION DES OUVERTURES SUPPLEMENTAIRES
    // ============================================================

    private void CreateRandomOpenings()
    {
        for (int i = 0; i < _nombreOuvertures * 30; i++)
        {
            // S'il n'y a plus de cellules disponibles
            if (_interiorCells.Count == 0)
            {
                return;
            }


            // Choisit une cellule intérieure au hasard
            int index = Random.Range(
                0,
                _interiorCells.Count
            );

            MazeCell cell = _interiorCells[index];


            // Crée une ouverture avec un voisin
            CreateOpening(cell);


            // Retire cette cellule de la liste
            // pour éviter de la sélectionner plusieurs fois
            _interiorCells.RemoveAt(index);
        }
    }


    // ============================================================
    // CREATION D'UNE OUVERTURE
    // ============================================================

    private void CreateOpening(MazeCell cell)
    {
        int x = (int)cell.transform.position.x;
        int z = (int)cell.transform.position.z;


        // Liste des directions disponibles
        List<int> directions = new List<int>();


        // DROITE
        if (x + 3 < _mazeWidth)
        {
            directions.Add(0);
        }


        // GAUCHE
        if (x - 3 >= 0)
        {
            directions.Add(1);
        }


        // DEVANT
        if (z + 3 < _mazeDepth)
        {
            directions.Add(2);
        }


        // DERRIERE
        if (z - 3 >= 0)
        {
            directions.Add(3);
        }


        // Sécurité
        if (directions.Count == 0)
        {
            return;
        }


        // Choisit une direction disponible
        int direction = directions[
            Random.Range(0, directions.Count)
        ];


        // ========================================================
        // DROITE
        // ========================================================

        if (direction == 0)
        {
            cell.ClearRightWall();

            _mazeGrid[x + 3, z].ClearLeftWall();
        }


        // ========================================================
        // GAUCHE
        // ========================================================

        else if (direction == 1)
        {
            cell.ClearLeftWall();

            _mazeGrid[x - 3, z].ClearRightWall();
        }


        // ========================================================
        // DEVANT
        // ========================================================

        else if (direction == 2)
        {
            cell.ClearFrontWall();

            _mazeGrid[x, z + 3].ClearBackWall();
        }


        // ========================================================
        // DERRIERE
        // ========================================================

        else if (direction == 3)
        {
            cell.ClearBackWall();

            _mazeGrid[x, z - 3].ClearFrontWall();
        }
    }

}

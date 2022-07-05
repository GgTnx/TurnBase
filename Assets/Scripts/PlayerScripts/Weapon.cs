namespace PlayerScripts
{
    public abstract class Weapon
    {
        // private void StartMove(List<Enemy> enemies)
        // {
        //     foreach (var enem in enemies)
        //     {
        //         CalculateMoveGrid(enem);
        //         SetTargetPlayer(_players, enem.transform.position);
        //         _grid.SetHighLight(path1);
        //         if (_target.transform.position.x < enem.transform.position.x)
        //         {
        //             Vector2 truTarget = path1.FirstOrDefault(x => x.x == path1.Min(y => y.x));
        //             var finish =Vector2Int.FloorToInt(truTarget);
        //             var start =Vector2Int.FloorToInt(enem.transform.position);
        //
        //             var path =_pathFinder.GetPath(start, finish);
        //             foreach (var VARIABLE in path1)
        //             {
        //                 print(VARIABLE);
        //             }
        //
        //             StartCoroutine(Move(enem, path));
        //
        //         }
        //         path1.Clear();
        //     }
        // }
        // oreach (var enem in enemies)
        // {
        //     // CalculateMoveGrid(enem);
        //     SetTargetPlayer(_players, enem.transform.position);
        //     // _grid.SetHighLight(path1);
        //     if (_target.transform.position.x < enem.transform.position.x)
        //     {
        //         enem._Renderer.flipX = true;
        //         //Vector2 truTarget = path1.FirstOrDefault(x => x.x == path1.Min(y => y.x));
        //         var finish =Vector2Int.FloorToInt(_target.transform.position);
        //         var start =Vector2Int.FloorToInt(enem.transform.position);
        //
        //         var path =_pathFinder.GetPath(start, finish);
        //         _camera.transform.position = enem.transform.position + new Vector3(0,0,-1);
        //
        //         yield return StartCoroutine(Move(enem, path));
        //
        //     }
        //     else if(_target.transform.position.x > enem.transform.position.x)
        //     {
        //         enem._Renderer.flipX = false;
        //         // Vector2 truTarget = path1.FirstOrDefault(x => x.x == path1.Max(y => y.x));
        //         var finish =Vector2Int.FloorToInt(_target.transform.position);
        //         var start =Vector2Int.FloorToInt(enem.transform.position);
        //         
        //         var path =_pathFinder.GetPath(start, finish);
        //         _camera.transform.position = enem.transform.position + new Vector3(0,0,-1);
        //         
        //         yield return StartCoroutine(Move(enem, path));
        //     }
        //     path1.Clear();
        // }
    }
}
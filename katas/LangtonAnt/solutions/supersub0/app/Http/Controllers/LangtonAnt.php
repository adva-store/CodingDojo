<?php

namespace App\Http\Controllers;

use App\Http\Controllers\LangtonAnt\Values;
use App\Models\Ant;
use App\Models\Grid;
use App\Services\LangtonAnt\Walker;
use Illuminate\Contracts\View\View;
use Illuminate\Http\Request;

class LangtonAnt extends Controller
{
    use Values;

    private readonly Walker $walker;

    public function __construct(?Walker $walker = null)
    {
        $this->walker = $walker ?? new Walker();
    }

    public function start(Request $request): View
    {
        $ant = new Ant();
        $ant->direction = $this->getDirection($request);
        $ant->position_x = $this->getPosition($request, 'position_x');
        $ant->position_y = $this->getPosition($request, 'position_y');
        $ant->save();

        $grid = new Grid();
        $grid->ant_id = $ant->id;
        $grid->size = $this->getSize($request);
        $grid->colors = $this->getColors($request);
        $grid->move_counter = $this->getMoveCounter($request);
        $grid->save();

        return view('langton-ant')
            ->with('grid', $grid)
            ->with('ant', $ant)
            ->with('speed', $request->get('speed', Ant::DEFAULT_SPEED));
    }

    public function walk(Request $request, int $id): View
    {
        $grid = Grid::find($id);
        $ant = $grid->ant();

        $this->walker->rotate($grid, $ant)
            ->colorize($grid, $ant)
            ->walk($grid, $ant);

        $ant->save();
        $grid->save();

        return view('langton-ant')
            ->with('grid', $grid)
            ->with('ant', $ant)
            ->with('speed', $request->get('speed', Ant::DEFAULT_SPEED))
            ->with('end', $this->walker->endOfWalk($grid, $ant));
    }
}

<?php

namespace Tests\Unit;

use App\Models\Ant;
use App\Models\Grid;
use App\Services\LangtonAnt\Walker;
use Tests\TestCase;

class WalkerTest extends TestCase
{
    /**
     * @dataProvider dataProviderRotate
     */
    public function testRotate(string $expected, string $direction, int $y, int $x): void
    {
        $grid = new Grid();
        $grid->colors = [
            [Grid::WHITE, Grid::WHITE, Grid::WHITE],
            [Grid::WHITE, Grid::WHITE, Grid::BLACK],
            [Grid::WHITE, Grid::WHITE, Grid::BLACK],
        ];

        $ant = new Ant();
        $ant->position_y = $y;
        $ant->position_x = $x;
        $ant->direction = $direction;

        (new Walker())->rotate($grid, $ant);

        $this->assertEquals($expected, $ant->direction);
    }

    public static function dataProviderRotate(): array
    {
        return [
            [
                Ant::DIRECTION_LEFT,
                Ant::DIRECTION_BOTTOM,
                1,
                1,
            ],
            [
                Ant::DIRECTION_TOP,
                Ant::DIRECTION_LEFT,
                1,
                1,
            ],
            [
                Ant::DIRECTION_RIGHT,
                Ant::DIRECTION_TOP,
                1,
                1,
            ],
            [
                Ant::DIRECTION_BOTTOM,
                Ant::DIRECTION_RIGHT,
                1,
                1,
            ],
            [
                Ant::DIRECTION_RIGHT,
                Ant::DIRECTION_BOTTOM,
                1,
                2,
            ],
            [
                Ant::DIRECTION_BOTTOM,
                Ant::DIRECTION_LEFT,
                1,
                2,
            ],
            [
                Ant::DIRECTION_LEFT,
                Ant::DIRECTION_TOP,
                1,
                2,
            ],
            [
                Ant::DIRECTION_TOP,
                Ant::DIRECTION_RIGHT,
                1,
                2,
            ],
        ];
    }

    /**
     * @dataProvider dataProviderColorize
     */
    public function testColorize(string $expected, int $y, int $x): void
    {
        $grid = new Grid();
        $grid->colors = [
            [Grid::WHITE, Grid::WHITE, Grid::WHITE],
            [Grid::WHITE, Grid::WHITE, Grid::BLACK],
            [Grid::WHITE, Grid::WHITE, Grid::BLACK],
        ];

        $ant = new Ant();
        $ant->position_y = $y;
        $ant->position_x = $x;

        (new Walker())->colorize($grid, $ant);

        $this->assertEquals($expected, $grid->colors[$y][$x]);
    }

    public static function dataProviderColorize(): array
    {
        return [
            [
                Grid::BLACK,
                1,
                1,
            ],
            [
                Grid::WHITE,
                1,
                2,
            ],
        ];
    }

    /**
     * @dataProvider dataProviderWalk
     */
    public function testWalk(int $expectedY, int $expectedX, string $direction, int $y, int $x): void
    {
        $grid = new Grid();

        $ant = new Ant();
        $ant->position_y = $y;
        $ant->position_x = $x;
        $ant->direction = $direction;

        (new Walker())->walk($grid, $ant);

        $this->assertEquals($expectedY, $ant->position_y);
        $this->assertEquals($expectedX, $ant->position_x);
    }

    public static function dataProviderWalk(): array
    {
        return [
            [
                2,
                1,
                Ant::DIRECTION_BOTTOM,
                1,
                1,
            ],
            [
                1,
                0,
                Ant::DIRECTION_LEFT,
                1,
                1,
            ],
            [
                0,
                1,
                Ant::DIRECTION_TOP,
                1,
                1,
            ],
            [
                1,
                2,
                Ant::DIRECTION_RIGHT,
                1,
                1,
            ],
        ];
    }

    /**
     * @dataProvider dataProviderEndOfWalk
     */
    public function testEndOfWalk(bool $expected, int $y, int $x): void
    {
        $grid = new Grid();
        $grid->size = 42;

        $ant = new Ant();
        $ant->position_y = $y;
        $ant->position_x = $x;

        $this->assertEquals($expected, (new Walker())->endOfWalk($grid, $ant));
    }

    public static function dataProviderEndOfWalk(): array
    {
        return [
            [
                true,
                42,
                1,
            ],
            [
                true,
                1,
                43,
            ],
            [
                true,
                -1,
                1,
            ],
            [
                true,
                1,
                -1,
            ],
            [
                false,
                1,
                1,
            ],
        ];
    }
}

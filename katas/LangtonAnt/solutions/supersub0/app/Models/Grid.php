<?php

namespace App\Models;

use App\Http\Controllers\LangtonAnt\Colors;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Grid extends Model
{
    use HasFactory;

    public const DEFAULT_SIZE = 11;
    public const DEFAULT_POSITION = 5;
    public const DEFAULT_MOVE_COUNTER = 0;
    public const DEFAULT_COLOR = Grid::WHITE;
    public const BLACK = 'b';
    public const WHITE = 'w';

    public $timestamps = false;

    protected $casts = [
        'colors' => 'array',
    ];

    public function ant(): ?Ant
    {
        return $this->belongsTo(Ant::class, 'ant_id')?->first();
    }
}

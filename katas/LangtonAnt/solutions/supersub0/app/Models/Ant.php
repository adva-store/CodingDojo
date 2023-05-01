<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Ant extends Model
{
    use HasFactory;

    public const DIRECTION_TOP = 't';
    public const DIRECTION_BOTTOM = 'b';
    public const DIRECTION_LEFT = 'l';
    public const DIRECTION_RIGHT = 'r';
    public const DEFAULT_DIRECTION = Ant::DIRECTION_BOTTOM;
    public const DEFAULT_SPEED = 100;

    public $timestamps = false;
}

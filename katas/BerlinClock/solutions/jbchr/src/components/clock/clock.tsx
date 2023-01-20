import { useTime } from "@/hooks/use-time";
import { computeBerlinClockRows } from "./clock.config";
import { FC } from "react";
import ClockBox from "../clock-box/clock-box";

const BOX_MARGIN_Y = 0.2;
const BOX_MARGIN_X = 0.1;
const ROW_WIDTH = 5;
const OFF_COLOR = 0x000000;
const ON_COLOR_YELLOW = 0xfcb603;
const ON_COLOR_RED = 0x962c02;

interface ClockProps {}

/**
 * Displays the berlin time block as a group of meshs
 */
const Clock: FC<ClockProps> = () => {
  const time = useTime();
  const rows = computeBerlinClockRows(ROW_WIDTH, BOX_MARGIN_X, BOX_MARGIN_Y);

  return (
    <group>
      <mesh rotation={[Math.PI / 2, 0, 0]} position={[0, 5.25, 0]}>
        <cylinderBufferGeometry attach="geometry" args={[1, 1, 1]} />
        <meshStandardMaterial color={time?.[4] ? ON_COLOR_YELLOW : OFF_COLOR} />
      </mesh>

      {rows.map((cols, rowCounter) =>
        cols.map((col, colCounter) => (
          <ClockBox
            key={colCounter + rowCounter * 10}
            width={col.width}
            position={col.position}
            color={
              !time || time[rowCounter] <= colCounter
                ? OFF_COLOR
                : /* Hours are red (row 2 + 3) as well as minutes  */
                rowCounter === 2 ||
                  rowCounter === 3 ||
                  (rowCounter === 1 && colCounter % 3 === 2)
                ? ON_COLOR_RED
                : ON_COLOR_YELLOW
            }
          ></ClockBox>
        ))
      )}
    </group>
  );
};

export default Clock;

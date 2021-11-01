<template>
  <div class="myCard text-white m-2">
    <img :src="keep.img" class="card-img" alt="..." />
    <div
      class="card-img-overlay"
      data-bs-toggle="modal"
      :data-bs-target="'#k-modal-' + keep.id"
      @click="viewCount(keep)"
    >
      <h5 class="card-title">{{ keep.name }}</h5>
      <img :src="keep.creator.picture" class="rounded-circle user-img" alt="" />
    </div>
  </div>
  <Modal :id="'k-modal-' + keep.id" class="text-light">
    <template #modal-body>
      <KeepInfo :keep="keep" class="" />
    </template>
  </Modal>
</template>

<script>
import { keepsService } from "../services/KeepsService";
import { Keep } from "../models/Keep";
import { computed } from "@vue/runtime-core";
import { AppState } from "../AppState";
import Pop from "../utils/Pop";
import { logger } from '../utils/Logger';
export default {
  props: {
    keep: {
      type: Keep,
      default: () => {
        return new Keep();
      },
    },
  },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async deleteKeep() {
        try {
          if (await Pop.confirm()) {
            await keepsService.deleteKeep(props.keep.id);
            Pop.toast("Keep Deleted");
          }
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error);
        }
      },
      async viewCount(keep) {
        try {
          keep.views = keep.views + 1;
          await keepsService.keepInteractions(keep);
        } catch (error) {
          Pop.toast(error.message, "error");
          logger.log(error)
        }
      },
    };
  },
};
</script>

<style lang="scss">
// body {
//   margin: 0;
//   padding: 1rem;
// }
.card-img-overlay {
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: end;
}
.myCard {
  position: relative;
  display: flex;
  flex-direction: column;
  min-width: 0;
  word-wrap: break-word;

  background-clip: border-box;

  border-radius: 0.25 rem;
}

.card-img {
  height: inherit;
  object-fit: cover;
}
.user-img {
  height: 45px;
}

// .masonry-with-columns {
//   columns: 6 200px;
//   column-gap: 1rem;
//   div {
//     width: 150px;
//     background: #EC985A;
//     color: white;
//     margin: 0 1rem 1rem 0;
//     display: inline-block;
//     width: 100%;
//     text-align: center;
//     font-family: system-ui;
//     font-weight: 900;
//     font-size: 2rem;
//   }
//   @for $i from 1 through 36 {
//     div:nth-child(#{$i}) {
//       $h: (random(400) + 100) + px;
//       height: $h;
//       line-height: $h;
//     }
//   }
// }
</style>

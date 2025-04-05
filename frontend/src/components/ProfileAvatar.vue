<template>
  <div class="text-center">
    <div class="avatar-container">
      <img
        :src="currentAvatar"
        @error="onImageError"
        alt="Avatar"
        class="rounded-circle img-thumbnail"
        width="200"
        height="200"
      />
    </div>
    <div class="mt-3">
      <button class="btn" @click="changeAvatar">
        Изменить
      </button>
    </div>
  </div>
</template>


<script>
import altavatar from '../pictures/altavatar.jpg';

export default {
  props: {
    avatar: {
      type: String,
      default: ''
    }
  },
  data() {
    return {
      currentAvatar: this.avatar || altavatar,
      fallbackAvatar: altavatar
    };
  },
  watch: {
    avatar(newVal) {
      this.currentAvatar = newVal || this.fallbackAvatar;
    }
  },
  methods: {
    onImageError() {
      this.currentAvatar = this.fallbackAvatar;
    },
    changeAvatar() {
      alert('Функция изменения аватарки пока не реализована.');
    }
  }
};
</script>

<style scoped>
.text-center {
  margin-top: 2%;
}
img {
  object-fit: cover;
}

.btn {
  background-color: #00b5c5;
  color: white;
}
.btn:hover {
  background-color: #037485;
  color: white;
}
</style>
